using System;


/// <summary>
/// https://gist.github.com/maraigue/739403
/// </summary>
[Serializable]
public struct Rational
{
    public int numerator;
    public int denominator;

    // コンストラクタ
    public Rational(int int_value) : this(int_value, 1) { }
    public Rational(int new_numerator, int new_denominator)
    {
        numerator = new_numerator;
        denominator = new_denominator;
    }
    
    // ---------- 補助関数 ----------

    // 最大公約数
    public static int gcd(int v1, int v2)
    {
        int tmp;

        // どちらかが0だったら即座に終了
        if (v1 == 0 || v2 == 0) return 0;

        // 正の値にしておく
        if (v1 < 0) v1 = -v1;
        if (v2 < 0) v2 = -v2;

        // v1の方を大きくしておく
        if (v2 > v1)
        {
            tmp = v1; v1 = v2; v2 = tmp;
        }

        for (;;)
        {
            tmp = v1 % v2;
            if (tmp == 0) return v2;

            v1 = v2; v2 = tmp;
        }
    }

    // 通分する
    private void _fix_denominator(Rational other)
    {
        int tmp = denominator;
        numerator *= other.denominator;
        denominator *= other.denominator;

        other.numerator *= tmp;
        other.denominator *= tmp;
    }

    // 正規化
    // ●分子・分母を約分する
    // ●負の符号が分母についている場合、分子にのみつけるようにする
    // ●値が0である場合は「0/1」にする
    private void _regularize()
    {
        int divisor = Math.Sign(denominator) * gcd(numerator, denominator);
        if (divisor == 0)
        {
            // 分子が0の場合
            numerator = 0;
            denominator = 1;
        }
        else
        {
            numerator /= divisor;
            denominator /= divisor;
        }
    }

    // ---------- 比較 ----------

    public static bool operator ==(Rational r1, Rational r2)
    {
        r1._regularize();
        r2._regularize();
        return (r1.numerator == r2.numerator && r1.denominator == r2.denominator);
    }

    public static bool operator !=(Rational r1, Rational r2)
    {
        return (r1 != r2);
    }

    public static bool operator <(Rational r1, Rational r2)
    {
        r1._fix_denominator(r2);
        return (r1.numerator < r2.numerator);
    }

    public static bool operator >(Rational r1, Rational r2)
    {
        return (r2 < r1);
    }

    // 以下、他の関数やクラスに使ってもらう際の補助関数
    public override bool Equals(object obj)
    {
        if (obj.GetType() == this.GetType())
        {
            return (this == (Rational)obj);
        }

        return false;
    }

    public override int GetHashCode()
    {
        return (numerator | (denominator << 16));
    }

    // ---------- 型変換 ----------

    public static implicit operator double(Rational r)
    {
        return (double)r.numerator / (double)r.denominator;
    }

    public static implicit operator float(Rational r)
    {
        return (float)r.numerator / (float)r.denominator;
    }

    public static implicit operator Rational(int i)
    {
        return new Rational(i);
    }

    // ---------- 加減乗除 ----------

    public static Rational operator +(Rational r) // 単項演算子
    {
        // 単に自分のコピーを作ればよい
        return new Rational(r.numerator, r.denominator);
    }

    public static Rational operator -(Rational r) // 単項演算子
    {
        return new Rational(-r.numerator, r.denominator);
    }

    public static Rational operator +(Rational r1, Rational r2) // 二項演算子
    {
        r1._fix_denominator(r2);
        return new Rational(r1.numerator + r2.numerator, r1.denominator);
    }

    public static Rational operator -(Rational r1, Rational r2) // 二項演算子
    {
        r1._fix_denominator(r2);
        return new Rational(r1.numerator - r2.numerator, r1.denominator);
    }

    public static Rational operator *(Rational r1, Rational r2)
    {
        return new Rational(r1.numerator * r2.numerator, r1.denominator * r2.denominator);
    }

    public static Rational operator /(Rational r1, Rational r2)
    {
        if (r2.numerator == 0)
        {
            throw new DivideByZeroException();
        }
        return new Rational(r1.numerator * r2.denominator, r1.denominator * r2.numerator);
    }

    // ユーティリティ
    public override string ToString()
    {
        _regularize();
        if (denominator == 1) return numerator.ToString();

        return string.Format("({0}/{1})", numerator, denominator);
    }
}
