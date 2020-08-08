using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class a_test_Fractals_KochGenerator_1 : MonoBehaviour {
    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    };

    [SerializeField]
    protected _axis axis = new _axis();

    protected enum _initiator
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon
    }

    public struct LineSegment
    {
        public Vector3 StartPositon { get; set; }
        public Vector3 EndPosition { get; set; }
        public Vector3 Direction { get; set; }
        public float Length { get; set; }
    }
    //Inspector에 노출되지 않기 때문에 SerializeField 처리.
    [SerializeField]
    protected _initiator initiator = new _initiator();
    [SerializeField]
    //선분의 형태를 변형하기위한 커브.
    protected AnimationCurve _generator;
    protected Keyframe[] _keys;
    [SerializeField]
    protected bool _useBezierCurves;
    [SerializeField]
    [Range(8,24)]
    protected int _bezierVertexCount;
    protected int _generationCount;

    protected int _initiatorPointAmount;
    private Vector3[] _initiatorPoint;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;
    //이동하는 기존 축 x,y,z와 수직이 되게 하기위한 시작 Rotation값 지정.
    private float _initialRotation;
    [SerializeField]
    protected float _initiatorSize;

    protected Vector3[] _position;
    protected Vector3[] _targetPosition;
    protected Vector3[] _bezierPosition;
    private List<LineSegment> _lineSegment;

    protected Vector3[] BezierCurve(Vector3[] points, int vertexCount)
    {
        var pointList = new List<Vector3>();
        // +=2를 하는 이유는 1 -> 3, 3 -> 5 식으로 중간점을 스킵하는 부드러운 선을 만들기 위함.
        for(int i = 0; i < points.Length; i += 2)
        {
            //*****
            if(i+2 <= points.Length - 1)
            {
                for (float ratio = 0f; ratio <= 1f; ratio += 1.0f / vertexCount)
                {
                    var tangentLineVertex1 = Vector3.Lerp(points[i], points[i + 1], ratio);
                    var tangentLineVertex2 = Vector3.Lerp(points[i + 1], points[i + 2], ratio);
                    var bezierPoint = Vector3.Lerp(tangentLineVertex1, tangentLineVertex2, ratio);
                    pointList.Add(bezierPoint);
                }
            }
        }
        return pointList.ToArray();
    }

    private void Awake()
    {
        GetInitiatorPoints();
        //assing lists & arrays
        // +1 은 마지막선(다시돌아가는선)을 위한 것.
        //LineRender를 위한 스크립트에서 다시 사용될것.
        _position = new Vector3[_initiatorPointAmount + 1];
        _targetPosition = new Vector3[_initiatorPointAmount + 1];
        _lineSegment = new List<LineSegment>();
        _keys = _generator.keys;

        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _position[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        _position[_initiatorPointAmount] = _position[0];
        _targetPosition = _position;
    }
    
    //bool값에 따라 선분안,밖으로 형태변환
    protected void KochGenerate (Vector3[] positions, bool outwards, float generatorMultiplier)
    {   
        // 선분 생성.
        _lineSegment.Clear();
        for(int i = 0; i < positions.Length - 1; i++)
        {
            LineSegment line = new LineSegment();
            line.StartPositon = positions[i];
            if(i == positions.Length - 1)
            {
               line.EndPosition = positions[0];
            }
            else
            {
               line.EndPosition = positions[i + 1];
            }
            line.Direction = (line.EndPosition - line.StartPositon).normalized;
            line.Length = Vector3.Distance(line.EndPosition, line.StartPositon);
            _lineSegment.Add(line);
        }
        // 선분을 point array에 더하기.
        List<Vector3> newPos = new List<Vector3>();
        List<Vector3> targetPos = new List<Vector3>();

        for(int i = 0; i < _lineSegment.Count; i++)
        {
            newPos.Add(_lineSegment[i].StartPositon);
            targetPos.Add(_lineSegment[i].StartPositon);

            for (int j = 1; j < _keys.Length - 1; j++)
            {
                float moveAmount = _lineSegment[i].Length * _keys[j].time;
                float heightAmount = (_lineSegment[i].Length * _keys[j].value) * generatorMultiplier;

                Vector3 movePos = _lineSegment[i].StartPositon + (_lineSegment[i].Direction * moveAmount);
                Vector3 Dir;
                if (outwards)
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
        newPos.Add(_lineSegment[0].StartPositon);
        targetPos.Add(_lineSegment[0].StartPositon);
        _position = new Vector3[newPos.Count];
        _targetPosition = new Vector3[targetPos.Count];
        _position = newPos.ToArray();
        _targetPosition = targetPos.ToArray();
        _bezierPosition = BezierCurve(_targetPosition, _bezierVertexCount);
        _generationCount++;
    }

    private void OnDrawGizmos()
    {

        GetInitiatorPoints();
        _initiatorPoint = new Vector3[_initiatorPointAmount];

        _rotateVector = Quaternion.AngleAxis(_initialRotation, _rotateAxis) * _rotateVector;
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _initiatorPoint[i] = _rotateVector * _initiatorSize;
            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;
        }
        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            Gizmos.color = Color.white;
            Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
            Gizmos.matrix = rotationMatrix;
            if (i < _initiatorPointAmount - 1)
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[i + 1]);
            }
            else
            {
                Gizmos.DrawLine(_initiatorPoint[i], _initiatorPoint[0]);
            }
        }
    }

    private void GetInitiatorPoints()
    {
        switch (initiator)
        {
            case _initiator.Triangle:
                _initiatorPointAmount = 3;
                _initialRotation = 0;
                break;

            case _initiator.Square:
                _initiatorPointAmount = 4;
                // 360(전체) / 4(선분) = 90(각도) / 2(반만돌리면됨) = 45
                // 360 / 4 = 90 / 2 =
                _initialRotation = 45;
                break;

            case _initiator.Pentagon:
                _initiatorPointAmount = 5;
                _initialRotation = 36;
                break;

            case _initiator.Hexagon:
                _initiatorPointAmount = 6;
                _initialRotation = 30;
                break;

            case _initiator.Heptagon:
                _initiatorPointAmount = 7;
                _initialRotation = 25.71428f;
                break;

            case _initiator.Octagon:
                _initiatorPointAmount = 8;
                _initialRotation = 22.5f;
                break;

            default:
                _initiatorPointAmount = 3;
                _initialRotation = 0;
                break;
        };

        switch (axis)
        {
            case _axis.XAxis:
                _rotateVector = new Vector3(1, 0, 0);
                _rotateAxis = new Vector3(0, 0, 1);
                break;

            case _axis.YAxis:
                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0);
                break;

            case _axis.ZAxis:
                _rotateVector = new Vector3(0, 0, 1);
                _rotateAxis = new Vector3(0, 1, 0);
                break;

            default:
                _rotateVector = new Vector3(0, 1, 0);
                _rotateAxis = new Vector3(1, 0, 0);
                break;
        }
    }
	void Start () {
		
	}
	
	void Update () {
		
	}
}
