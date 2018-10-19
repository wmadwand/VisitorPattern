using System;
using System.Collections.Generic;

namespace VisitorMS01
{
	// operation-visitor
	public interface IVisitor
	{
		void VisitPersonAccount(Person account);
		void VisitCompanyAccount(Company account);
	}

	// concrete operation-visitor A
	public class HTMLVisitor : IVisitor
	{
		public void VisitCompanyAccount(Company account)
		{
			Console.WriteLine($"HTML: Company name {account.title}, Head name: {account.directorName}");
		}

		public void VisitPersonAccount(Person account)
		{
			Console.WriteLine($"HTML: Person name {account.name}, age: {account.age}");
		}
	}

	// concrete operation-visitor B
	public class XMLVisitor : IVisitor
	{
		public void VisitCompanyAccount(Company account)
		{
			Console.WriteLine($"XML: Company name {account.title}, Head name: {account.directorName}");
		}

		public void VisitPersonAccount(Person account)
		{
			Console.WriteLine($"XML: Person name {account.name}, age: {account.age}");
		}
	}

	// element from structure
	public interface IAccount
	{
		void Accept(IVisitor visitor);
	}

	// concrete element from structure A
	public class Person : IAccount
	{
		public string name;
		public int age;

		public void Accept(IVisitor visitor)
		{
			visitor.VisitPersonAccount(this);
		}
	}

	// concrete element from structure B
	public class Company : IAccount
	{
		public string title;
		public string directorName;
		public int age;

		public void Accept(IVisitor visitor)
		{
			visitor.VisitCompanyAccount(this);
		}
	}

	// structure
	public class Bank
	{
		private List<IAccount> _accounts = new List<IAccount>();

		public void Add(IAccount acc)
		{
			_accounts.Add(acc);
		}

		public void Remove(IAccount acc)
		{
			_accounts.Remove(acc);
		}

		public void Accept(IVisitor vis)
		{
			_accounts.ForEach(x => x.Accept(vis));
		}
	}

	class Program
	{
		static void Main(string[] args)
		{
			Bank structure = new Bank();
			structure.Add(new Person { name = "John", age = 44 });
			structure.Add(new Person { name = "Mark", age = 33 });
			structure.Add(new Company { title = "Trust bank", directorName = "Anderson", age = 64 });

			structure.Accept(new HTMLVisitor());
			structure.Accept(new XMLVisitor());
		}
	}
}