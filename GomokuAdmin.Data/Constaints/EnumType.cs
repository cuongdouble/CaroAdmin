using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GomokuAdmin.Data.Constraints
{
    public enum GameType
    {
        [PgName("normal")]
        Normal,
        [PgName("timeout")]
        Timeout,
        [PgName("surrender")]
        Surrender,
        [PgName("quit")]
        Quit,
    }          

    public enum GameResult
    {
        [PgName("0")]
        X,
        [PgName("1")]
        Y,
        [PgName("2")]
        Draw
    }

    public enum TeamSide
    {
        [PgName("0")]
        X,
        [PgName("1")]
        Y
    }
}
