#ifndef TONE_INCLUDED
#define TONE_INCLUDED

half4 _Tone;

half3 CalcTone(half3 c)
{
	return lerp(c, _Tone.x * pow(abs(c), _Tone.y) + _Tone.z, _Tone.w);
}

#endif
