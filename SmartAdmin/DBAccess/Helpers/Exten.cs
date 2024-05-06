using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

public static class Exten
{
    public static string ExecuteOutputParam(this IDbConnection conn, string sql, object args)
    {
        sql += ", @RetValue OUT";

        var p = new DynamicParameters();

        var properties = (Dictionary<string, object>)args;
        foreach (var prop in properties)
        {
            p.Add(prop.Key, prop.Value == null ? "" : prop.Value);
        }

        p.Add("RetValue", "", DbType.String, ParameterDirection.Output, 50);

        conn.Execute(sql, p);

        string id = p.Get<string>("RetValue");
        return id;
    }

    public static async Task<string> ExecuteOutputParamAsync(this IDbConnection conn, string sql, object args)
    {
        sql += ", @RetValue OUT";

        var p = new DynamicParameters();

        var properties = (Dictionary<string, object>)args;
        foreach (var prop in properties)
        {
            p.Add(prop.Key, prop.Value == null ? "" : prop.Value);
        }

        p.Add("RetValue", "", DbType.String, ParameterDirection.Output, 50);

        await conn.ExecuteAsync(sql, p);

        string id = p.Get<string>("RetValue");
        return id;
    }

    public static string FillParam(this string query, object args)
    {
        StringBuilder paraText = new StringBuilder();

        var properties = (Dictionary<string, object>)args;
        foreach (var prop in properties)
        {
            if (paraText.ToString() == "")
            {
                paraText.Append($" @{prop.Key}");
            }
            else
            {
                paraText.Append($", @{prop.Key}");
            }
        }

        return $"{query}{paraText}";
    }
}
