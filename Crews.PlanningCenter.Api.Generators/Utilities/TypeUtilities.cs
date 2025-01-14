using System.Reflection;

namespace Crews.PlanningCenter.Api.Generators.Utilities;

static class TypeUtilities
{
	public static Type? FindTypeInAllAssemblies(string fullName)
	{
		Type? type = Type.GetType(fullName);
		if (type != null) return type;

		type = GetTypeFromAppDomain(fullName);
		if (type != null) return type;

		LoadAssembliesFromBaseDirectory();
		return GetTypeFromAppDomain(fullName);
	}

	private static Type? GetTypeFromAppDomain(string fullName)
		=> AppDomain.CurrentDomain
			.GetAssemblies()
			.Select(assembly => assembly.GetType(fullName))
			.FirstOrDefault(t => t != null);

	private static void LoadAssembliesFromBaseDirectory()
	{
		string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
		string[] dllFiles = Directory.GetFiles(baseDirectory, "*.dll");

		foreach (string dllFile in dllFiles)
		{
			try
			{
				AssemblyName assemblyName = AssemblyName.GetAssemblyName(dllFile);
				if (!AppDomain.CurrentDomain.GetAssemblies().Any(a => a.GetName().FullName == assemblyName.FullName))
				{
					Assembly.Load(assemblyName);
				}
			}
			catch { continue; }
		}
	}
}
