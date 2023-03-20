using EasMe.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace EasMe.Extensions;

public static class EntityFrameworkExtensions
{
    /// <summary>
    ///     Truncates DbSet or Table, this action can not be undone.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dbSet"></param>
    public static void Clear<T>(this DbSet<T> dbSet) where T : class
    {
        dbSet.RemoveRange(dbSet);
    }


    public static void ClearAndSaveChanges<T>(this DbSet<T> dbSet, DbContext _dbContext) where T : class
    {
        dbSet.RemoveRange(dbSet);
        _dbContext.SaveChanges();
    }

    /// <summary>
    ///     Saves changes to db _dbContext, if no rows affected throws exception
    /// </summary>
    /// <param name="dbContext"></param>
    /// <exception cref="InternalDbException"></exception>
    public static int SaveChangesOrThrow(this DbContext dbContext,
        string message = "No changes were applied to the database.")
    {
        var affected = dbContext.SaveChanges();
        if (affected == 0)
            throw new InternalDbException(message);
        return affected;
    }
}