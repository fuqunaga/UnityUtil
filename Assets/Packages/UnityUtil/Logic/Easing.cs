using UnityEngine;
using System.Collections;

public enum EasingType{
	EaseNone, 
	EaseInQuad,  	EaseOutQuad,  						EaseOutInQuad,
	EaseInCubic, 	EaseOutCubic, 	EaseInOutCubic, 	EaseOutInCubic,
	EaseInQuart, 	EaseOutQuart, 	EaseInOutQuart, 	EaseOutInQuart,
	EaseInQuint, 	EaseOutQuint, 	EaseInOutQuint, 	EaseOutInQuint,
	EaseInSine,	 	EaseOutSine,  	EaseInOutSine,  	EaseOutInSine,
	EaseInExpo,  	EaseOutExpo,  	EaseInOutExpo,  	EaseOutInExpo,
	EaseInCirc,	 	EaseOutCirc,  	EaseInOutCirc,  	EaseOutInCirc,
	EaseInBounce,	EaseOutBounce,	EaseInOutBounce,	EaseOutInBounce,
	EaseInBack,	 	EaseOutBack,  	EaseInOutBack,  	EaseOutInBack,
	EaseInElastic,	EaseOutElastic, EaseInOutElastic, 	EaseOutInElastic,
	EaseInAtan,		EaseOutAtan,	EaseInOutAtan
}

/* this class is derived from Cinder's Timeline API to perform different kinds of easing animation */
public class Easing : MonoBehaviour {

	//master function for using EasingType with default parameters
	public static float easeWithType( EasingType type, float t ){
		switch (type) {
		case EasingType.EaseNone:
			return easeNone(t);
		case EasingType.EaseInQuad:
			return easeInQuad(t);
		case EasingType.EaseOutQuad:
			return easeOutQuad(t);
		case EasingType.EaseOutInQuad:
			return easeOutInQuad(t);
		case EasingType.EaseInCubic:
			return easeInCubic(t);
		case EasingType.EaseOutCubic:
			return easeOutCubic(t);
		case EasingType.EaseInOutCubic:
			return easeInOutCubic(t);
		case EasingType.EaseOutInCubic:
			return easeOutInCubic(t);
		case EasingType.EaseInQuart:
			return easeInQuart(t);
		case EasingType.EaseOutQuart:
			return easeOutQuart(t);
		case EasingType.EaseInOutQuart:
			return easeInOutQuart(t);
		case EasingType.EaseOutInQuart:
			return easeOutInQuart(t);
		case EasingType.EaseInQuint:
			return easeInQuint(t);
		case EasingType.EaseOutQuint:
			return easeOutQuint(t);
		case EasingType.EaseInOutQuint:
			return easeInOutQuint(t);
		case EasingType.EaseOutInQuint:
			return easeOutInQuint(t);
		case EasingType.EaseInSine:
			return easeInSine(t);
		case EasingType.EaseOutSine:
			return easeOutSine(t);
		case EasingType.EaseInOutSine:
			return easeInOutSine(t);
		case EasingType.EaseOutInSine:
			return easeOutInSine(t);
		case EasingType.EaseInExpo:
			return easeInExpo(t);
		case EasingType.EaseOutExpo:
			return easeOutExpo(t);
		case EasingType.EaseInOutExpo:
			return easeInOutExpo(t);
		case EasingType.EaseOutInExpo:
			return easeOutInExpo(t);
		case EasingType.EaseInCirc:
			return easeInCirc(t);
		case EasingType.EaseOutCirc:
			return easeOutCirc(t);
		case EasingType.EaseInOutCirc:
			return easeInOutCirc(t);
		case EasingType.EaseOutInCirc:
			return easeOutInCirc(t);
		case EasingType.EaseInBounce:
			return easeInBounce(t);
		case EasingType.EaseOutBounce:
			return easeOutBounce(t);
		case EasingType.EaseInOutBounce:
			return easeInOutBounce(t);
		case EasingType.EaseOutInBounce:
			return easeOutInBounce(t);
		case EasingType.EaseInBack:
			return easeInBack(t);
		case EasingType.EaseOutBack:
			return easeOutBack(t);
		case EasingType.EaseInOutBack:
			return easeInOutBack(t);
		case EasingType.EaseOutInBack:
			return easeOutInBack(t, 1.70158f);
		case EasingType.EaseInElastic:
			return easeInElastic(t, 1.25f, 1.5f); 
		case EasingType.EaseOutElastic:
			return easeOutElastic(t, 1.25f, 1.5f);
		case EasingType.EaseInOutElastic:
			return easeInOutElastic(t, 1.25f, 1.5f);
		case EasingType.EaseOutInElastic:
			return easeOutInElastic(t, 1.25f, 1.5f);
		case EasingType.EaseInAtan:
			return easeInAtan(t);
		case EasingType.EaseOutAtan:
			return easeOutAtan(t);
		case EasingType.EaseInOutAtan:
			return easeInOutAtan(t);
		}
		return t;
	}

	//! Easing equation for a simple linear tweening with no easing.
	public static float easeNone( float t ){
		return t;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Quadratic
	//! Easing equation for a quadratic (t^2) ease-in, accelerating from zero velocity.
	public static float easeInQuad( float t ){
		return t*t;
	}
		
	//! Easing equation for a quadratic (t^2) ease-out, decelerating to zero velocity.
	public static float easeOutQuad( float t ){ 
		return -t * ( t - 2 );
	}

	//! Easing equation for a quadratic (t^2) ease-in/out, accelerating until halfway, then decelerating.
	public float easeInOutQuad( float t ){
		t *= 2;
		if( t < 1 ) return 0.5f * t * t;
		t -= 1;
		return -0.5f * ((t)*(t-2) - 1);
	}
		
	//! Easing equation for a quadratic (t^2) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInQuad( float t ){
		if( t < 0.5f) return easeOutQuad( t*2 ) * 0.5f;
		return easeInQuad( (2*t)-1 ) * 0.5f + 0.5f;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Cubic
		
	//! Easing equation function for a cubic (t^3) ease-in, accelerating from zero velocity.
	public static float easeInCubic( float t ){
		return t*t*t;
	}
		
	//! Easing equation for a cubic (t^3) ease-out, decelerating to zero velocity.
	public static float easeOutCubic( float t ){
		t -= 1;
		return t*t*t + 1;
	}
		
	//! Easing equation for a cubic (t^3) ease-in/out, accelerating until halfway, then decelerating.
	public static float easeInOutCubic( float t ){
		t *= 2;
		if( t < 1 )
			return 0.5f * t*t*t;
		t -= 2;
		return 0.5f*(t*t*t + 2);
	}

	//! Easing equation for a cubic (t^3) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInCubic( float t ){
		if( t < 0.5f ) return easeOutCubic( 2 * t ) / 2;
		return easeInCubic(2*t - 1)/2 + 0.5f;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Quartic
		
	//! Easing equation for a quartic (t^4) ease-in, accelerating from zero velocity.
	public static float easeInQuart( float t ){
		return t*t*t*t;
	}

	//! Easing equation for a quartic (t^4) ease-out, decelerating to zero velocity.
	public static float easeOutQuart( float t ){
		t -= 1;
		return -(t*t*t*t - 1);
	}
		
	//! Easing equation for a quartic (t^4) ease-in/out, accelerating until halfway, then decelerating.
	public static float easeInOutQuart( float t ){
		t *= 2;
		if( t < 1 ) return 0.5f*t*t*t*t;
		else {
			t -= 2;
			return -0.5f * (t*t*t*t - 2);
		}
	}

	//! Easing equation for a quartic (t^4) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInQuart( float t ){
		if( t < 0.5f ) return easeOutQuart( 2*t ) / 2;
		return easeInQuart(2*t-1)/2 + 0.5f;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Quintic
		
	//! Easing equation function for a quintic (t^5) ease-in, accelerating from zero velocity.
	public static float easeInQuint( float t ){
		return t*t*t*t*t;
	}
		
	//! Easing equation for a quintic (t^5) ease-out, decelerating to zero velocity.
	public static float easeOutQuint( float t ){
		t -= 1;
		return t*t*t*t*t + 1;
	}
		
	//! Easing equation for a quintic (t^5) ease-in/out, accelerating until halfway, then decelerating.
	public static float easeInOutQuint( float t ){
		t *= 2;
		if( t < 1 ) return 0.5f*t*t*t*t*t;
		else {
			t -= 2;
			return 0.5f*(t*t*t*t*t + 2);
		}
	}
		
	//! Easing equation for a quintic (t^5) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInQuint( float t ){
		if( t < 0.5f ) return easeOutQuint( 2*t ) / 2;
		return easeInQuint( 2*t - 1 ) / 2 + 0.5f;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Sine
		
	//! Easing equation for a sinusoidal (sin(t)) ease-in, accelerating from zero velocity.
	public static float easeInSine( float t ){
		return -Mathf.Cos( t * (float)Mathf.PI / 2 ) + 1;
	}
		
	//! Easing equation for a sinusoidal (sin(t)) ease-out, decelerating from zero velocity.
	public static float easeOutSine( float t ){
		return Mathf.Sin( t * (float)Mathf.PI / 2 );
	}
		
	//! Easing equation for a sinusoidal (sin(t)) ease-in/out, accelerating until halfway, then decelerating.
	public static float easeInOutSine( float t ){
		return -0.5f * ( Mathf.Cos( (float)Mathf.PI * t ) - 1 ); 
	}
		
	//! Easing equation for a sinusoidal (sin(t)) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInSine( float t ){
		if( t < 0.5f ) return easeOutSine( 2 * t ) / 2;
		return easeInSine( 2*t - 1 ) / 2 + 0.5f;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Exponential
		
	//! Easing equation for an exponential (2^t) ease-in, accelerating from zero velocity.
	public static float easeInExpo( float t ){
		return t == 0 ? 0 : (Mathf.Pow( 2, 10 * (t - 1) ));
	}

	//! Easing equation for an exponential (2^t) ease-out, decelerating from zero velocity.
	public static float easeOutExpo( float t ){
		return t == 1 ? 1 : (- Mathf.Pow( 2.0f, -10 * t ) + 1);
	}
		
	//! Easing equation for an exponential (2^t) ease-in/out, accelerating until halfway, then decelerating.
	public static float easeInOutExpo( float t ){
		if( t == 0 ) return 0;
		if( t == 1 ) return 1;
		t *= 2;
		if( t < 1 ) return 0.5f * Mathf.Pow( 2, 10 * (t - 1) );
		return 0.5f * ( - Mathf.Pow( 2, -10 * (t - 1)) + 2);
	}
		
	//! Easing equation for an exponential (2^t) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInExpo( float t ){
		if( t < 0.5f ) return easeOutExpo( 2 * t ) / 2;
		return easeInExpo( 2 * t - 1 ) / 2 + 0.5f;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Circular
		
	//! Easing equation for a circular (sqrt(1-t^2)) ease-in, accelerating from zero velocity.
	public static float easeInCirc( float t ){
		return -( Mathf.Sqrt( 1 - t*t ) - 1);
	}
		
	//! Easing equation for a circular (sqrt(1-t^2)) ease-out, decelerating from zero velocity.
	public static float easeOutCirc( float t ){
		t -= 1;
		return Mathf.Sqrt( 1 - t*t );
	}

	//! Easing equation for a circular (sqrt(1-t^2)) ease-in/out, accelerating until halfway, then decelerating.
	public static float easeInOutCirc( float t ){
		t *= 2;
		if( t < 1 ) {
			return -0.5f * (Mathf.Sqrt( 1 - t*t ) - 1);
		}else {
			t -= 2;
			return 0.5f * (Mathf.Sqrt( 1 - t*t ) + 1);
		}
	}

	//! Easing equation for a circular (sqrt(1-t^2)) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInCirc( float t ){
		if( t < 0.5f ) return easeOutCirc( 2*t ) / 2;
		return easeInCirc( 2*t - 1 ) / 2 + 0.5f;
	}
		
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Bounce
	//! \cond
	public static float easeOutBounceHelper_( float t, float c, float a ){
		if( t == 1 ) return c;
		if( t < (4/11.0f) ) {
			return c*( 7.5625f*t*t);
		}else if( t < (8/11.0f) ) {
			t -= (6/11.0f);
			return -a * (1 - (7.5625f*t*t + 0.75f)) + c;
		}else if( t < (10/11.0f) ) {
			t -= (9/11.0f);
			return -a * (1 - (7.5625f*t*t + 0.9375f)) + c;
		}else {
			t -= (21/22.0f);
			return -a * (1 - (7.5625f*t*t + 0.984375f)) + c;
		}
	}
	//! \endcond
		
	//! Easing equation for a bounce (exponentially decaying parabolic bounce) ease-in, accelerating from zero velocity. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeInBounce( float t, float a = 1.70158f ){
		return 1 - easeOutBounceHelper_( 1-t, 1, a );
	}

	//! Easing equation for a bounce (exponentially decaying parabolic bounce) ease-out, decelerating from zero velocity. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeOutBounce( float t, float a = 1.70158f ){
		return easeOutBounceHelper_( t, 1, a );
	}
		
	//! Easing equation for a bounce (exponentially decaying parabolic bounce) ease-in/out, accelerating until halfway, then decelerating. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeInOutBounce( float t, float a = 1.70158f ){
		if( t < 0.5f ) return easeInBounce( 2*t, a ) / 2;
		else return ( t == 1 ) ? 1 : easeOutBounce( 2*t - 1, a )/2 + 0.5f;
	}
		
	//! Easing equation for a bounce (exponentially decaying parabolic bounce) ease-out/in, decelerating until halfway, then accelerating. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeOutInBounce( float t, float a = 1.70158f ){
		if( t < 0.5f ) return easeOutBounceHelper_( t*2, 0.5f, a );
		return 1 - easeOutBounceHelper_( 2 - 2*t, 0.5f, a );
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Back
		
	//! Easing equation for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) ease-in, accelerating from zero velocity. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeInBack( float t, float s = 1.70158f ){
		return t * t * ((s+1)*t - s);
	}
		
	//! Easing equation for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) ease-out, decelerating from zero velocity. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeOutBack( float t, float s = 1.70158f ){ 
		t -= 1;
		return (t*t*((s+1)*t + s) + 1);
	}
		
	//! Easing equation for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) ease-in/out, accelerating until halfway, then decelerating. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeInOutBack( float t, float s = 1.70158f ){
		t *= 2;
		if( t < 1 ) {
			s *= 1.525f;
			return 0.5f*(t*t*((s+1)*t - s));
		}else {
			t -= 2;
			s *= 1.525f;
			return 0.5f*(t*t*((s+1)*t+ s) + 2);
		}
	}

	//! Easing equation for a back (overshooting cubic easing: (s+1)*t^3 - s*t^2) ease-out/in, decelerating until halfway, then accelerating. The \a a parameter controls overshoot, the default producing a 10% overshoot.
	public static float easeOutInBack( float t, float s ){
		if( t < 0.5f ) return easeOutBack( 2*t, s ) / 2;
		return easeInBack( 2*t - 1, s )/2 + 0.5f;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Elastic
		
	//! \cond
	public static float easeInElasticHelper_( float t, float b, float c, float d, float a, float p ){
		if( t == 0 ) return b;
		float t_adj = t / d;
		if( t_adj == 1 ) return b+c;
			
		float s;
		if( a < Mathf.Abs(c) ) {
			a = c;
			s = p / 4.0f;
		} else {
			s = p / (2 * (float)Mathf.PI) * Mathf.Asin( c / a );
		}
		t_adj -= 1;
		return -( a * Mathf.Pow( 2,10*t_adj) * Mathf.Sin( (t_adj*d-s)*(2*(float)Mathf.PI)/p )) + b;
	}
		
	public static float easeOutElasticHelper_( float t, float b, float c, float d, float a, float p ){
		if( t == 0 ) return 0;
		if( t == 1) return c;
		float s;
		if( a < c ) {
			a = c;
			s = p / 4;
		}else {
			s = p / ( 2 * (float)Mathf.PI ) * Mathf.Asin( c / a );
		}
		return a * Mathf.Pow( 2, -10*t ) * Mathf.Sin( (t-s)*(2*(float)Mathf.PI)/p ) + c;
	}
	//! \endcond
		
	//! Easing equation for an elastic (exponentially decaying sine wave) ease-in, accelerating from zero velocity.
	public static float easeInElastic( float t, float amplitude, float period ){
		return easeInElasticHelper_( t, 0, 1, 1, amplitude, period );
	}
		
	//! Easing equation for an elastic (exponentially decaying sine wave) ease-out, decelerating from zero velocity.
	public static float easeOutElastic( float t, float amplitude, float period ){
		return easeOutElasticHelper_( t, 0, 1, 1, amplitude, period );
	}
		
	//! Easing equation for an elastic (exponentially decaying sine wave) ease-in/out, accelerating until halfway, then decelerating.
	public static float easeInOutElastic( float t, float amplitude, float period ){
		if( t == 0 ) return 0;
		t *= 2;
		if( t == 2 ) return 1;
			
		float s;
		if( amplitude < 1 ) {
			amplitude = 1;
			s = period / 4;
		}else {
			s = period / (2 * (float)Mathf.PI) * Mathf.Asin( 1 / amplitude );
		}
			
		if( t < 1 ) return -0.5f * ( amplitude * Mathf.Pow( 2.0f, 10*(t-1) ) * Mathf.Sin( (t-1-s)*(2*(float)Mathf.PI)/period ));
		return amplitude * Mathf.Pow( 2,-10*(t-1) ) * Mathf.Sin( (t-1-s)*(2*(float)Mathf.PI)/period ) * 0.5f + 1;
	}
		
	//! Easing equation for an elastic (exponentially decaying sine wave) ease-out/in, decelerating until halfway, then accelerating.
	public static float easeOutInElastic( float t, float amplitude, float period ){
		if (t < 0.5) return easeOutElasticHelper_(t*2, 0, 0.5f, 1.0f, amplitude, period );
		return easeInElasticHelper_(2*t - 1, 0.5f, 0.5f, 1, amplitude, period );
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	// Atan
		
	//! Easing equation for an atan ease-in, accelerating from zero velocity. Used by permssion from Chris McKenzie.
	public static float easeInAtan( float t, float a = 15 ){
		float m = Mathf.Atan( a );
		return ( Mathf.Atan( (t - 1)*a ) / m ) + 1;
	}

	//! Easing equation for an atan ease-out, decelerating from zero velocity. Used by permssion from Chris McKenzie.
	public static float easeOutAtan( float t, float a = 15 ){
		float m = Mathf.Atan( a );
		return Mathf.Atan( t*a ) / m;
	}

	//! Easing equation for an atan ease-in/out, accelerating until halfway, then decelerating. Used by permssion from Chris McKenzie.
	public static float easeInOutAtan( float t, float a = 15 ){
		float m = Mathf.Atan( 0.5f * a );
		return ( Mathf.Atan((t - 0.5f)*a) / (2*m) ) + 0.5f;
	}
}
