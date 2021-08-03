namespace Vitrivr.UnityInterface.CineastApi.Model.Query
{
  public enum RelationalOperator
  {
    /// TRUE if A is equal to B.
    Eq,

    /// TRUE if A is not equal to B.
    NEq,

    /// TRUE if A is greater than or equal to B.
    GEq,

    /// TRUE if A is less than or equal to B.
    LEq,

    /// TRUE if A is greater than B.
    Greater,

    /// TRUE if A is less than B.
    Less,

    /// TRUE if A is between B and C
    Between,

    /// TRUE if string A matches string B (SQL LIKE syntax expected).
    Like,

    /// TRUE if string A does not match string B (SQL LIKE syntax expected).
    NLike,

    /// TRUE for fulltext match; Apache Lucene syntax expected.
    Match,

    /// TRUE if A is null.
    IsNull,

    /// TRUE if A is not null.
    IsNotNull,

    In
  }
}