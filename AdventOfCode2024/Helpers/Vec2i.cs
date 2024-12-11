﻿namespace AdventOfCode2024.Helpers;

public struct Vec2i : IEquatable<Vec2i>
{
    #region Public Constructors

    public Vec2i(int x, int y)
    {
        X = x;
        Y = y;
    }

    #endregion Public Constructors

    #region Public Properties

    public int X { get; set; }

    public int Y { get; set; }

    #endregion Public Properties

    #region Public Methods

    public static Vec2i operator +(Vec2i lhs, Vec2i rhs)
    {
        return new Vec2i(lhs.X + rhs.X, lhs.Y + rhs.Y);
    }

    public bool Equals(Vec2i other)
    {
        return X == other.X && Y == other.Y;
    }

    public override bool Equals(object? obj)
    {
        return obj is Vec2i other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y);
    }

    #endregion Public Methods
}