#ifndef LINE_SEGMENT_HLSL
#define LINE_SEGMENT_HLSL

struct LineSegment
{
	float2 start;
	float2 end;
};


bool IsLeft(float2 pos, LineSegment ls)
{
	float2 start = ls.start;
	float2 end = ls.end;

	float2 toEnd = end - start;
	float2 toPos = pos - start;

	return cross(float3(toEnd,0), float3(toPos,0)).z > 0;
}

bool IsRight(float2 pos, LineSegment ls)
{
	float2 start = ls.start;
	float2 end = ls.end;

	float2 toEnd = end - start;
	float2 toPos = pos - start;

	return cross(float3(toEnd,0), float3(toPos,0)).z < 0;
}

// 点との距離の二乗
// https://zenn.dev/boiledorange73/articles/0037-js-distance-pt-seg
float LineDistanceSq(float2 pos, LineSegment ls)
{
	float2 start = ls.start;
	float2 end = ls.end;

	float2 posToStart = start - pos;

	float2 ab = end - start;
	float r2 = dot(ab, ab);
	float tt = -dot(ab, posToStart);
	if (tt < 0)
	{
		return dot(posToStart, posToStart);
	}
	
	if (tt > r2)
	{
		float2 posToEnd = end - pos;
		return dot(posToEnd, posToEnd);
	}
	
	float f1 = ab.x * posToStart.y - ab.y * posToStart.x;
	return (f1 * f1) / r2;
}

float LineDistance(float2 pos, LineSegment ls)
{
	return sqrt(LineDistanceSq(pos, ls));
}

float LineClosestPointRate(float2 pos, LineSegment ls)
{
	float2 start = ls.start;
	float2 end = ls.end;

	float2 posToStart = start - pos;

	float2 ab = end - start;
	float r2 = dot(ab, ab);
	float tt = -dot(ab, posToStart);

	return saturate(tt/r2);
}

inline float2 CalcPointOnLineFromRate(LineSegment ls, float rateOnSegment)
{
	return lerp(ls.start, ls.end, rateOnSegment);
}

float2 LineClosestPoint(float2 pos, LineSegment ls)
{
	float rate = LineClosestPointRate(pos, ls);
	return CalcPointOnLineFromRate(ls, rate);
}

float2 GetNormal(LineSegment ls)
{
	float2 dir = ls.end - ls.start;
	float2 lineNormal = float2( -dir.y, dir.x); // rotate 90 degree
	return normalize(lineNormal);
}


//交点を求める
//https://www.hiramine.com/programming/graphics/2d_segmentintersection.html
//https://gist.github.com/yoshiki/7702066
bool CalcIntersection(LineSegment lhs, LineSegment rhs, out float2 hitPosition)
{
	hitPosition = (0).xx;
	
	float2 a = lhs.start;
	float2 b = lhs.end;
	float2 c = rhs.start;
	float2 d = rhs.end;

	float div = (b.x - a.x) * (d.y - c.y) - (b.y - a.y) * (d.x - c.x);

	float2 ac = c - a;
	float u = ((d.y - c.y) * ac.x - (d.x - c.x) * ac.y) / div;
	float s = ((b.y - a.y) * ac.x - (b.x - a.x) * ac.y) / div;

	if (!isfinite(u) || u < 0.0 || 1.0 < u) return false;
	if (!isfinite(u) || s < 0.0 || 1.0 < s) return false;

	hitPosition = a + u * (b - a);
	return true;
}

#endif // LINE_SEGMENT_HLSL