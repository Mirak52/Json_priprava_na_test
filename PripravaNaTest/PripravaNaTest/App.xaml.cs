using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PripravaNaTest
{
    /// <summary>
    /// Interakční logika pro App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static PersonDatabase _customers;

        public static PersonDatabase DatabasePersons
        {
            get
            {
                if (_customers == null)
                {
                    var fileHelper = new FileHelper();
                    _customers = new PersonDatabase(fileHelper.GetLocalFilePath("TodoSQLite.db3"));
                }
                return _customers;
            }
        }
    }
}
