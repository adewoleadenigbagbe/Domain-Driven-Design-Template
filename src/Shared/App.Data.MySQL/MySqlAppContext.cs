using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using App.Data.Contexts;
using MySql.Data.EntityFramework;

namespace App.Data.MySQL
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlReadWriteAppContext : ReadWriteAppContext
    {
    }

    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlReadAppContext : ReadAppContext
    {
    }

}
