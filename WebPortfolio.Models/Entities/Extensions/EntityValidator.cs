using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebPortfolio.Core.DataAccess.Abstract;
using WebPortfolio.Core.Repositories;

namespace WebPortfolio.Models.Entities.Extensions
{
    public abstract class EntityValidator<T>
        //where T: class, IEntity
    {

        private IList<Rule<T>> _rules { get; set; }

        public EntityValidator()
        {
            _rules = new List<Rule<T>>();
        }

        public void Add(Func<T, bool> validator, string errorMessage)
        {
            _rules.Add(new Rule<T> { Validator = validator, ErrorMessage = errorMessage });
        }

        public void Add(Func<T, bool> validator, string propName, string errorMessage)
        {
            _rules.Add(new Rule<T> { Validator = validator, ErrorMessage = errorMessage, PropertyName = propName });
        }

        public IList<string> Validate(T obj)
        {
            return _rules.Where(x => !x.Validator(obj)).Select(x => x.ErrorMessage).ToList();
        }

        public bool IsValid(T obj)
        {
            return _rules.Any(x => !x.Validator(obj));
        }

    }

    public class Rule<T>
    {
        public Func<T, bool> Validator { get; set; }

        public string ErrorMessage { get; set; }

        public string PropertyName { get; set; }
    }
}
