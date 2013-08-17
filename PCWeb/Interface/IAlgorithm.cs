using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PCWeb.Interface
{
    public interface IAlgorithm
    {
        string Hash(string inputText);
    }
}