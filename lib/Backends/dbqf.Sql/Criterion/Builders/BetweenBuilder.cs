﻿using dbqf.Criterion;
using dbqf.Criterion.Builders;
using dbqf.Criterion.Values;

namespace dbqf.Sql.Criterion.Builders
{
    public class BetweenBuilder : ParameterBuilder, IBetweenBuilder
    {
        public BetweenBuilder()
        {
            Label = "between";
        }

        /// <summary>
        /// Works with values that are BetweenValue objects.  
        /// Only processes the first value.  Use JunctionBuilder to combine multiple.
        /// </summary>
        public override IParameter Build(IFieldPath path, params object[] values)
        {
            if (values == null || values.Length == 0)
                return null;

            var v = values[0] as BetweenValue;
            if (v != null)
            {
                if (v.From != null && v.To != null)
                    return new SqlBetweenParameter(path, v.From, v.To);
                else if (v.To == null)
                    return new SqlSimpleParameter(path, ">=", v.From);
                else if (v.From == null)
                    return new SqlSimpleParameter(path, "<=", v.To);
            }

            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is BetweenBuilder)
            {
                var other = (BetweenBuilder)obj;
                return base.Eq(this.Label, other.Label);
            }
            return base.Equals(obj);
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
