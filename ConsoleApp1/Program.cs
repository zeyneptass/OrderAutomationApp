// See https://aka.ms/new-console-template for more information
using DataAccess.Concrete.EntityFramework;
using System.Reflection;
using System.Windows;
using System.Windows.Forms;

Console.WriteLine("Hello, World!");
 using (CafeContext context = new CafeContext())
    {
        var foreignKeys = from fk in context.GetEntityTypes().SelectMany(e => e.GetForeignKeys())
                          select new
                          {
                              ParentObjectName = fk.PrincipalToDependent?.DeclaringEntityType.Name,
                              ReferencedObjectName = fk.PrincipalEntityType.Name,
                              ForeignKeyName = fk.Relational().ConstraintName
                          };

        string message = "Bağımlılıklar:\n\n";

        foreach (var fk in foreignKeys)
        {
            message += $"Parent Object: {fk.ParentObjectName}\nReferenced Object: {fk.ReferencedObjectName}\nForeign Key Name: {fk.ForeignKeyName}\n\n";
        }

        MessageBox.Show(message, "Bağımlılıklar", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
