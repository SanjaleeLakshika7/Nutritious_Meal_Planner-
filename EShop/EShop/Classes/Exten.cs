﻿using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Data;
using Newtonsoft.Json;
using System.Web;

// Extension methods to write code easy

public static class Exten
{
    public static string ToClass(this bool status)
    {
        return status ? "" : "d-none";
    }

    public static string ToMultiline(this string text)
    {
        return text.Replace(Environment.NewLine, "<br/>");
    }

    public static string ExecuteOutputParam(this IDbConnection conn, string sql, object args)
    {
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

    public static SelectList ToSelectList<T>(this List<T> objList, string ID, string Name)
    {
        return new SelectList(objList, ID, Name);
    }

    public static T GetObject<T>(this ISession session, string key)
    {
        var data = session.GetString(key);
        if (data == null)
        {
            return default(T);
        }
        return JsonConvert.DeserializeObject<T>(data);
    }

    public static void SetObject(this ISession session, string key, object value)
    {
        session.SetString(key, JsonConvert.SerializeObject(value));
    }

    public static string ToShort(this string instance, int MaxSize)
    {
        var len = instance.Length;
        if (len > MaxSize)
        {
            return instance.Substring(0, MaxSize) + "...";
        }
        else
        {
            return instance;
        }
    }

    public static bool ToShortcount(this string instance, int MaxSize)
    {
        var len = instance.Length;
        if (len > MaxSize)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}