using Microsoft.EntityFrameworkCore;
using nCoVMSApi.Common;
using nCoVMSApi.Entity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace nCoVMSApi.Entity
{
    public class DBFactory
    {
        //Scaffold-DbContext  "Data Source=120.77.169.141;Initial Catalog=nCoVMaterialStore;User ID=sa;Password=hotpoint@2019" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Entity\Models -Force -DataAnnotations -StartupProject nCoVMSApi -Context nCoVMSDBContext  -Project nCoVMSApi

        public static nCoVMSDBContext nCoVMS(string connectionKey = "connectionstrings:nCoVMSConnection")
        {
            string connectionstring = ConfigHelper.Get(connectionKey ?? "connectionstrings:nCoVMSConnection", "Data Source=120.77.169.141;Initial Catalog=nCoVMaterialStore;User ID=sa;Password=hotpoint@2019;MultipleActiveResultSets=true");

            DbContextOptions<nCoVMSDBContext> dbContextOption = new DbContextOptions<nCoVMSDBContext>();
            DbContextOptionsBuilder<nCoVMSDBContext> dbContextOptionBuilder = new DbContextOptionsBuilder<nCoVMSDBContext>(dbContextOption);
            return new nCoVMSDBContext(dbContextOptionBuilder.UseSqlServer(connectionstring).Options);
        }

    }
}
