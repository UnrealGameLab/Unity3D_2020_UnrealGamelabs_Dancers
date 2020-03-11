using System.Collections;
using System.Collections.Generic;
using UnityEngine;





// FART -- Not the MAIN SCript in  which my FRACTALS are Working OK 
// This SCRIPT copy created again on --- 31 JAN -- 2300H 












// Comments in this script -- which start with -- FOO_Video_Num -- identify which line or chunk of code was written to this file during which Tutorial video on YouTube
public class a_test_Fractals_KochGenerator : MonoBehaviour
{
    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    };

    [SerializeField]
    protected _axis axis = new _axis();

    protected enum _initiator // Here -- protected enum _initiator -- means only this class andf its methods can access
    {
        Triangle,
        Triangle_1,
        Square,
        Hexagon,
        Pentagon,
        Heptagon,
        Octagon
    };

      public struct LineSegment
    {
        public Vector3 StartPosition { get; set; } // FOO_TBD -- provide public get and set methods, through properties, to access and update the value of a private field
        public Vector3 EndPosition { get; set; }
        public Vector3 Direction { get; set; }
        public float Length { get; set; }
    }


    [SerializeField]
    protected _initiator initiator = new _initiator();
    [SerializeField]
    protected AnimationCurve _generator; // This creates a dedicated UI like element with a DopeSheet kind of effect 
    // We can then create - Animations with a TIMELINE upon the existing vertices / LINES of our 2D shapes 
    protected Keyframe[] _keys; // its an ARRAY named - Keyframe   

    [SerializeField]
    protected bool _useBezierCurves;
    [SerializeField]
    [Range(8, 24)]
    protected int _bezierVertexCount;

     [System.Serializable]
    public struct StartGen
    {
        public bool outwards;
        public float scale;
    }
    public StartGen[] _startGen;
    // // This is the ARRAY which shall store Values for the 2 VARIABLES- outwards and SCALE 
    // We also Init this in a FOR Loop below 








    
    protected int _generationCount; // We shall do MULTIPLE generations - thus we create a COUNTER to count the generations

    protected int _initiatorPointAmount;
    private Vector3[]  _initiatorPoint; // here - Vector3[]  , is an ARRAY 
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    // above - we need a ROTATE FACTOR == _rotateVector, to hold the rotatioon value
    private float _initialRotation;
    // Above - line added - as we see in EDITOR , when we change the SELECTED AXIS in the Inspector 
    // the SQUARE doesnt align with the Gizmos X Axis . The Gizmo which shows the TRANSFORM AXIS in the middle 
    // of the EDITOR screen should align with a SIDE of the SQUARE .
    // So basis how many -- _initiatorPointAmount <<Initiator Points >> are passed in , 
    // the SQUARE / PENTAGON etc should ROTATE and align with the Axis passed in . 


    [SerializeField] // So that we can see the FIELD == _initiatorSize , in the Unity EDitor -- INSPECTOR
    protected float _initiatorSize; // the SIZE of the - TRIANGLE etc figures 

    protected Vector3[] _position;
    protected Vector3[] _targetPosition; 
    protected Vector3[] _bezierPosition;
    private List<LineSegment> _lineSegment; // FOO_Video_Num_Vid3 -- List created in Video -3 


    protected Vector3[] BezierCurve(Vector3[] points, int vertexCount)
    {
        var pointList = new List<Vector3>();
        for (int i = 0; i < points.Length; i+=2) // WHY -- 1+=2 -- We skip One VERTEX on the Polygon and go to next Vertex and connect
        {
            if (i+2 <= points.Length - 1)
            {
                for (float ratio = 0f; ratio <= 1f; ratio += 1.0f/vertexCount)
                {
                    var tangentLineVertex1 = Vector3.Lerp(points[i], points[i + 1], ratio);
                    var tangentLineVertex2 = Vector3.Lerp(points[i+1], points[i + 2], ratio);
                    var bezierpoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
                    pointList.Add(bezierpoint);
                }
            }
        }
        return pointList.ToArray();
    }





    private void Awake()
    
    {
        GetInitiatorPoints();
        //Assign lists & arrays
        _position = new Vector3[_initiatorPointAmount + 1]; // the +1 -- is for last line segment returning to the START Point
        _targetPosition = new Vector3[_initiatorPointAmount + 1];
        _keys = _generator.keys; // the Number of KEYS in the Animation Curve ==>> protected AnimationCurve _generator; 
        _lineSegment = new List<LineSegment>(); // A LineSegment is ONE VERTICE of the 2D Shape - we need its , length , direction etc.
        // FOO_Video_Num_Vid3 -- List created in Video -3 
      

        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;

        for (int i = 0; i < _initiatorPointAmount; i++) //FOO_TBD -- this here loops through all SHAPES ?? 
        {
            _position[i] = _rotateVector * _initiatorSize; // here its -- _position[i] ---iterating over position-- down below in for loop its == _initiatorPoint[i]
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        _position[_initiatorPointAmount] = _position[0]; // this is the last position in the ARRAY plus One -- as the ARRAY is starting at ZERO
        _targetPosition = _position;

        // FOO_Changed_in_Video_Part_V ---
         for (int i = 0; i < _startGen.Length; i++)
        {
            KochGenerate(_targetPosition, _startGen[i].outwards, _startGen[i].scale);
        }

    }   // Created another Script-- KochLine.cs --to assign Line_Positions / Line_Points to the _position
    

    
    protected void KochGenerate(Vector3[] positions, bool outwards, float generatorMultiplier) 
    // this BOOL value, decides  whether generated Line Segment generated OUTWARDS or INWARDS == bool outwards
    // this FLOAT generatorMultiplier, decides height of generated Line Segment, within Animation Curve ==>> protected AnimationCurve _generator; 
    {
        // Firstly like a __INIT__ clear the LIST / delete if any values within the LIST
        _lineSegment.Clear();
        // Now going to populate the LIST with ELEMENTS which here are == LineSegments , so that we start generating shapes
        for (int i = 0; i < positions.Length - 1; i++)
                {
                    LineSegment line = new LineSegment();
                    line.StartPosition = positions[i]; // The CURRENT position - or the CURRENT Element from the ARRAY == 
                    if (i == positions.Length - 1) // If POSITION is LAST position of - positions ARRAY , then set next position to ZERO
                    {
                        line.EndPosition = positions[0]; // from above --- then set next position to ZERO
                    } 
                    else
                    {
                        line.EndPosition = positions[i + 1];
                    }
                    line.Direction = (line.EndPosition - line.StartPosition).normalized; // We get a NORMALIZED Vector back here ...
                    line.Length = Vector3.Distance(line.EndPosition, line.StartPosition);
                    _lineSegment.Add(line); // The ELEMENT - LINE has been appended with all the PARAM values and is now ADDED to the _lineSegment
                }
                //add line segments to point array
                // Below - Two temporary lists of - New Positions and Target Positions
                List<Vector3> newPos = new List<Vector3>();
                List<Vector3> targetPos = new List<Vector3>();

                for (int i = 0; i < _lineSegment.Count; i++) // Loop through all the LINE SEGMENTS
                        {
                            newPos.Add(_lineSegment[i].StartPosition);
                            targetPos.Add(_lineSegment[i].StartPosition);

                            // Below - we loop through all the KEYS that we create in the UI Curve --->> AnimationCurve _generator
                            // The KEY at 0.33 is the 1st KEY , the KEY at 0.55 is the 2nd KEY 
                            // Dont need LAST of the KEY's as its always default == 1 , thus middle PARAM of ITERATOR is -->> j < _keys.Length - 1
                            for (int j = 1; j < _keys.Length - 1; j++) 
                            {
                                float moveAmount = _lineSegment[i].Length * _keys[j].time;
                                float heightAmount = (_lineSegment[i].Length * _keys[j].value) * generatorMultiplier;
                                Vector3 movePos= _lineSegment[i].StartPosition + (_lineSegment[i].Direction * moveAmount);
                                Vector3 Dir;
                                if(outwards)
                                {
                                    Dir = Quaternion.AngleAxis(-90, _rotateAxis) * _lineSegment[i].Direction;
                                }
                                else
                                {
                                    Dir = Quaternion.AngleAxis(90, _rotateAxis) * _lineSegment[i].Direction;
                                }
                                newPos.Add(movePos);
                                targetPos.Add(movePos + (Dir * heightAmount));
                            }
                        }
                         newPos.Add(_lineSegment[0].StartPosition);
                        targetPos.Add(_lineSegment[0].StartPosition);
                        _position = new Vector3[newPos.Count];
                        _targetPosition = new Vector3[targetPos.Count];
                        _position = newPos.ToArray(); // Conversion of LIST to ARRAY
                        _targetPosition = targetPos.ToArray(); // Conversion of LIST to ARRAY
                        _bezierPosition = BezierCurve(_targetPosition, _bezierVertexCount);
                        _generationCount++; // One entire generation done -- thus generation counter is incremented
                
    } // ENDS --protected void KochGenerator(Vector3[] positions, bool outwards, float generatorMultiplier)

    private void OnDrawGizmos()
    {
        GetInitiatorPoints(); 
        // Now we will - Place a point in a certain direction 
        // point will have a certain length..
        // then rotate the factor by 360-Degree / Divided by the value of _initiatorPointAmount
        _initiatorPoint = new Vector3[_initiatorPointAmount];
        //_rotateVector = new Vector3(0,0,1); // IInd Video --Commented Out --- Replaced above with the ENUM -- which gets called below in SWITCH
        //_rotateAxis = new Vector3(0,1,0); // Rotation happening on Y-AXIS 
        // IInd Video --Commented Out Above line --- Replaced above with the ENUM -- which gets called below in SWITCH
        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;
        // Above line of code same as below -- have replaced -->> ***360 / _initiatorPointAmount*** with -->> ***_initialRotation***
        // Above -- Quaternion.AngleAxis == Creates a rotation which rotates angle degrees around axis.

        for (int i =0 ; i < _initiatorPointAmount; i++)
        {
            _initiatorPoint[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        
        for (int i =0 ; i < _initiatorPointAmount; i++)
        {
            Gizmos.color = Color.red;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position , transform.rotation , transform.lossyScale); 
            // above -- TRS == Trasform Rotation and lossySCALE 
            Gizmos.matrix = rotationMatrix;
            

            if( i < _initiatorPointAmount -1 )
            {
                Gizmos.DrawLine(_initiatorPoint[i] , _initiatorPoint[i+1]);
            } // Check if the line being drawn is the last line --- if not then ELSE below
            else
            {
                Gizmos.DrawLine(_initiatorPoint[i] , _initiatorPoint[0]);
                // If the Line being Drawn is the LAST Line --- we are going back to LINE ZERO
            }

        }
        


    }
    private void GetInitiatorPoints()
    {
        switch(initiator)
        {
            case _initiator.Triangle:
            _initiatorPointAmount = 3;
            _initialRotation = 0; // No value for - _initialRotation, as the TRIANGLE aligns just fine. 
            break;

            case _initiator.Triangle_1:
            _initiatorPointAmount = 12;
            _initialRotation = 11.5f; // No value for - _initialRotation, as the TRIANGLE aligns just fine. 
            break;
            

            case _initiator.Square:
            _initiatorPointAmount = 4; // if we delete this here --  _initiatorPointAmount -- we will get a CIRCLE inplace of SQUARE
            _initialRotation =45; // 360 / 4 = 90 / 2 = 45 degrees
            break;

            case _initiator.Pentagon:
            _initiatorPointAmount =5;
            _initialRotation = 36;
            break;

            case _initiator.Hexagon:
            _initiatorPointAmount =6;
            _initialRotation = 30;
            break;
            
            case _initiator.Heptagon:
            _initiatorPointAmount =7;
            _initialRotation = 25.71428f;
            break;
            case _initiator.Octagon:
            _initiatorPointAmount =8;
            _initialRotation = 22.5f;
            break;
            default:
            _initiatorPointAmount =3;
            _initialRotation = 0;
            break;
    
        }; // Ends SWITCH Statement 

        switch (axis)
        {
            case _axis.XAxis:
                _rotateVector = new Vector3(1, 0, 0);
                _rotateAxis = new Vector3(0, 0, 1); // here it will rotate on the Z-Axis
                break;

            case _axis.YAxis:
                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0); // here it will rotate on the X-Axis
                break;

            case _axis.ZAxis:
                _rotateVector = new Vector3(0, 0, 1);
                _rotateAxis = new Vector3(0, 1, 0); // here it will rotate on the Y-Axis
                break;

            default: // Same as YAxis above 
                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0);
                break;
        }; // Ends SWITCH Statement ==switch (axis)
    } // Ends -- private void GetInitiatorPoints()




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
