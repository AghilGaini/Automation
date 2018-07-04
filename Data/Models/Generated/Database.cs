




















// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: `Automation`
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Data Source=.;Initial Catalog=TestAutomation;Integrated Security=False;User Id=sa;password=**zapped**;MultipleActiveResultSets=True`
//     Schema:                 ``
//     Include Views:          `True`



using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Data.Models.Generated.Automation
{

	public partial class AutomationDB : Database
	{
		public AutomationDB() 
			: base("Automation")
		{
			CommonConstruct();
		}

		public AutomationDB(string connectionStringName) 
			: base(connectionStringName)
		{
			CommonConstruct();
		}
		
		partial void CommonConstruct();
		
		public interface IFactory
		{
			AutomationDB GetInstance();
		}
		
		public static IFactory Factory { get; set; }
        public static AutomationDB GetInstance()
        {
			if (_instance!=null)
				return _instance;
				
			if (Factory!=null)
				return Factory.GetInstance();
			else
				return new AutomationDB();
        }

		[ThreadStatic] static AutomationDB _instance;
		
		public override void OnBeginTransaction()
		{
			if (_instance==null)
				_instance=this;
		}
		
		public override void OnEndTransaction()
		{
			if (_instance==this)
				_instance=null;
		}
        

		public class Record<T> where T:new()
		{
			public static AutomationDB repo { get { return AutomationDB.GetInstance(); } }
			public bool IsNew() { return repo.IsNew(this); }
			public object Insert() { return repo.Insert(this); }

			public void Save() { repo.Save(this); }
			public int Update() { return repo.Update(this); }

			public int Update(IEnumerable<string> columns) { return repo.Update(this, columns); }
			public static int Update(string sql, params object[] args) { return repo.Update<T>(sql, args); }
			public static int Update(Sql sql) { return repo.Update<T>(sql); }
			public int Delete() { return repo.Delete(this); }
			public static int Delete(string sql, params object[] args) { return repo.Delete<T>(sql, args); }
			public static int Delete(Sql sql) { return repo.Delete<T>(sql); }
			public static int Delete(object primaryKey) { return repo.Delete<T>(primaryKey); }
			public static bool Exists(object primaryKey) { return repo.Exists<T>(primaryKey); }
			public static bool Exists(string sql, params object[] args) { return repo.Exists<T>(sql, args); }
			public static T SingleOrDefault(object primaryKey) { return repo.SingleOrDefault<T>(primaryKey); }
			public static T SingleOrDefault(string sql, params object[] args) { return repo.SingleOrDefault<T>(sql, args); }
			public static T SingleOrDefault(Sql sql) { return repo.SingleOrDefault<T>(sql); }
			public static T FirstOrDefault(string sql, params object[] args) { return repo.FirstOrDefault<T>(sql, args); }
			public static T FirstOrDefault(Sql sql) { return repo.FirstOrDefault<T>(sql); }
			public static T Single(object primaryKey) { return repo.Single<T>(primaryKey); }
			public static T Single(string sql, params object[] args) { return repo.Single<T>(sql, args); }
			public static T Single(Sql sql) { return repo.Single<T>(sql); }
			public static T First(string sql, params object[] args) { return repo.First<T>(sql, args); }
			public static T First(Sql sql) { return repo.First<T>(sql); }
			public static List<T> Fetch(string sql, params object[] args) { return repo.Fetch<T>(sql, args); }
			public static List<T> Fetch(Sql sql) { return repo.Fetch<T>(sql); }
			public static List<T> Fetch(long page, long itemsPerPage, string sql, params object[] args) { return repo.Fetch<T>(page, itemsPerPage, sql, args); }
			public static List<T> Fetch(long page, long itemsPerPage, Sql sql) { return repo.Fetch<T>(page, itemsPerPage, sql); }
			public static List<T> SkipTake(long skip, long take, string sql, params object[] args) { return repo.SkipTake<T>(skip, take, sql, args); }
			public static List<T> SkipTake(long skip, long take, Sql sql) { return repo.SkipTake<T>(skip, take, sql); }
			public static Page<T> Page(long page, long itemsPerPage, string sql, params object[] args) { return repo.Page<T>(page, itemsPerPage, sql, args); }
			public static Page<T> Page(long page, long itemsPerPage, Sql sql) { return repo.Page<T>(page, itemsPerPage, sql); }
			public static IEnumerable<T> Query(string sql, params object[] args) { return repo.Query<T>(sql, args); }
			public static IEnumerable<T> Query(Sql sql) { return repo.Query<T>(sql); }

		}

	}
	



    

	[TableName("dbo.Privilges")]



	[PrimaryKey("ID")]




	[ExplicitColumns]

    public partial class Privilge : AutomationDB.Record<Privilge>  
    {
	public struct Columns
	{
	
	public static String  ID  = @"ID";
	
	public static String  Gref  = @"Gref";
	
	public static String  Gid  = @"Gid";
	
	public static String  Title  = @"Title";
	
	}



		[Column] public long ID { get; set; }





		[Column] public Guid? Gref { get; set; }





		[Column] public Guid Gid { get; set; }





		[Column] public string Title { get; set; }



	}

    

	[TableName("dbo.RolePrivilges")]



	[PrimaryKey("ID")]




	[ExplicitColumns]

    public partial class RolePrivilge : AutomationDB.Record<RolePrivilge>  
    {
	public struct Columns
	{
	
	public static String  ID  = @"ID";
	
	public static String  RoleID  = @"RoleID";
	
	public static String  PrivilegeID  = @"PrivilegeID";
	
	}



		[Column] public long ID { get; set; }





		[Column] public long RoleID { get; set; }





		[Column] public long PrivilegeID { get; set; }



	}

    

	[TableName("dbo.Roles")]



	[PrimaryKey("ID")]




	[ExplicitColumns]

    public partial class Role : AutomationDB.Record<Role>  
    {
	public struct Columns
	{
	
	public static String  ID  = @"ID";
	
	public static String  RoleName  = @"RoleName";
	
	public static String  RoleLevel  = @"RoleLevel";
	
	}



		[Column] public long ID { get; set; }





		[Column] public string RoleName { get; set; }





		[Column] public long RoleLevel { get; set; }



	}

    

	[TableName("dbo.UserRole")]



	[PrimaryKey("ID")]




	[ExplicitColumns]

    public partial class UserRole : AutomationDB.Record<UserRole>  
    {
	public struct Columns
	{
	
	public static String  ID  = @"ID";
	
	public static String  UserID  = @"UserID";
	
	public static String  RoleID  = @"RoleID";
	
	}



		[Column] public long ID { get; set; }





		[Column] public long UserID { get; set; }





		[Column] public long RoleID { get; set; }



	}

    

	[TableName("dbo.Users")]



	[PrimaryKey("ID")]




	[ExplicitColumns]

    public partial class User : AutomationDB.Record<User>  
    {
	public struct Columns
	{
	
	public static String  ID  = @"ID";
	
	public static String  Username  = @"Username";
	
	public static String  Name  = @"Name";
	
	public static String  Family  = @"Family";
	
	public static String  Email  = @"Email";
	
	public static String  Address  = @"Address";
	
	public static String  Mobile  = @"Mobile";
	
	public static String  IsActive  = @"IsActive";
	
	public static String  IsManager  = @"IsManager";
	
	public static String  salt  = @"salt";
	
	public static String  Password  = @"Password";
	
	public static String  RoleID  = @"RoleID";
	
	}



		[Column] public long ID { get; set; }





		[Column] public string Username { get; set; }





		[Column] public string Name { get; set; }





		[Column] public string Family { get; set; }





		[Column] public string Email { get; set; }





		[Column] public string Address { get; set; }





		[Column] public string Mobile { get; set; }





		[Column] public bool? IsActive { get; set; }





		[Column] public bool? IsManager { get; set; }





		[Column] public Guid? salt { get; set; }





		[Column] public string Password { get; set; }





		[Column] public long? RoleID { get; set; }



	}

    

	[TableName("dbo.VwUserPrivilegeRole")]




	[ExplicitColumns]

    public partial class VwUserPrivilegeRole : AutomationDB.Record<VwUserPrivilegeRole>  
    {
	public struct Columns
	{
	
	public static String  RoleID  = @"RoleID";
	
	public static String  RoleLevel  = @"RoleLevel";
	
	public static String  Gid  = @"Gid";
	
	public static String  UserID  = @"UserID";
	
	public static String  Name  = @"Name";
	
	public static String  IsActive  = @"IsActive";
	
	}



		[Column] public long RoleID { get; set; }





		[Column] public long RoleLevel { get; set; }





		[Column] public Guid Gid { get; set; }





		[Column] public long UserID { get; set; }





		[Column] public string Name { get; set; }





		[Column] public bool? IsActive { get; set; }



	}


}
