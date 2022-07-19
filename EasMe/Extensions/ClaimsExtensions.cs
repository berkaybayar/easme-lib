using EasMe.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
        public static ClaimsIdentity ConvertModelToClaimsIdentity<T>(T Model)
        {
            try
            {
                var claimsIdentity = new ClaimsIdentity();
                foreach (var property in Model.GetType().GetProperties())
                {
                    if (property == null) continue;
                    var value = property.GetValue(Model);
                    if (value == null) continue;
                    claimsIdentity.AddClaim(new Claim(property.Name, value.ToString()));
                }
                return claimsIdentity;
            }
            catch (Exception ex)
            {
                throw new FailedToConvertException("EasJWT failed to convert model to claims identity.", ex);
            }

        }
        /// <summary>
        /// Converts given model to claims identity. Outs exceptions if there is an error occurs while converting one of the properties. This adds one claim that 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="Model"></param>
        /// <param name="ExceptionMessage"></param>
        /// <returns></returns>
        /// <exception cref="FailedToConvertException"></exception>
        public static ClaimsIdentity ConvertModelToClaimsIdentity<T>(T Model, out List<Exception> ExceptionMessages)
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
        public static T ConvertClaimsIdentityToModel<T>(ClaimsIdentity claimsIdentity, out List<Exception> ExceptionMessages)
        {

            ExceptionMessages = new();
            var model = Activator.CreateInstance<T>();
            foreach (var property in model.GetType().GetProperties())
            {

                try
                {

                    if (property == null) continue;
                    var value = claimsIdentity.FindFirst(property.Name);
                    if (value == null) continue;
                    property.SetValue(model, value.Value.StringConversion<T>());
                }
                catch (Exception ex)
                {
                    ExceptionMessages.Add(ex);

                }
            }
            return model;

        }

        public static T ConvertClaimsIdentityToModel<T>(ClaimsIdentity claimsIdentity)
        {
            try
            {
                var model = Activator.CreateInstance<T>();
                foreach (var property in model.GetType().GetProperties())
                {
                    if (property == null) continue;
                    var value = claimsIdentity.FindFirst(property.Name);
                    if (value == null) continue;
                    property.SetValue(model, value.Value.StringConversion<T>());
                }
                return model;

            }
            catch (Exception ex)
            {
                throw new FailedToConvertException("EasJWT failed to convert claims identity to model.", ex);
            }
        }
    }
}
