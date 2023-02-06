using EasMe.Exceptions;
using System.ComponentModel;
using System.Security.Claims;

namespace EasMe.Extensions
{
    public static class ClaimsExtensions
    {
		/// <summary>
		/// Converts given model to claims identity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Model"></param>
		/// <returns></returns>
		/// <exception cref="FailedToConvertException"></exception>
		public static Dictionary<string,object> AsDictionary(this IEnumerable<Claim> claims)
		{
            var dic = new Dictionary<string, object>();  
            //var type = claims.GetType();
            //var props = type.GetProperties();
            foreach(var item in claims)
            {
                var name = item.Type;
                var value = item.Value;
                dic[name] = value;
            }
            return dic;

		}


		/// <summary>
		/// Converts given model to claims identity.
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="Model"></param>
		/// <returns></returns>
		/// <exception cref="FailedToConvertException"></exception>
		public static ClaimsIdentity ToClaimsIdentity<T>(this T Model)
        {
            var claimsIdentity = new ClaimsIdentity();
            var props = Model?.GetType().GetProperties();
            if (props == null) throw new FailedToConvertException("Failed to convert model to claims identity. Model has no properties");
            foreach (var property in props)
            {
                if (property == null) continue;
                var value = property.GetValue(Model);
                if (value == null) continue;
                claimsIdentity.AddClaim(new Claim(property.Name, value.ToString()));
            }
            return claimsIdentity;

        }
        /// <summary>
        /// Converts given model to claims identity. Outs exceptions if there is an error occurs while converting one of the properties. This adds one claim that 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="ExceptionMessage"></param>
        /// <returns></returns>
        /// <exception cref="FailedToConvertException"></exception>
        public static ClaimsIdentity ToClaimsIdentity<T>(this T Model, out List<Exception> ExceptionMessages)
        {
            ExceptionMessages = new();
            var claimsIdentity = new ClaimsIdentity();
            foreach (var property in Model.GetType().GetProperties())
            {
                try
                {
                    if (property == null) continue;
                    var value = property.GetValue(Model);
                    if (value == null) continue;
                    claimsIdentity.AddClaim(new Claim(property.Name, value.ToString()));

                }
                catch (Exception ex)
                {
                    ExceptionMessages.Add(ex);
                }

            }
            return claimsIdentity;



        }
    

	}

}
