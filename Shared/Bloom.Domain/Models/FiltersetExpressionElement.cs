﻿using System;
using System.Data.Linq.Mapping;
using Bloom.Domain.Enums;
using Bloom.Domain.Interfaces;

namespace Bloom.Domain.Models
{
    /// <summary>
    /// Represents an element in a filterset.
    /// </summary>
    [Table(Name = "filterset_element")] //TODO
    public class FiltersetExpressionElement
    {
        /// <summary>
        /// Creates a new filterset element instance.
        /// </summary>
        /// <param name="filterset">A filterset.</param>
        /// <param name="elementType">The type of element.</param>
        /// <param name="elementNumber">The element number.</param>
        public static FiltersetExpressionElement Create(Filterset filterset, FiltersetExpressionElementType elementType, int elementNumber)
        {
            return new FiltersetExpressionElement
            {
                FiltersetId = filterset.Id,
                ElementType = elementType,
                ElementNumber = elementNumber
            };
        }

        /// <summary>
        /// Creates a new filterset element instance.
        /// </summary>
        /// <param name="filterset">A filterset.</param>
        /// <param name="filter">The filter.</param>
        /// <param name="elementNumber">The element number.</param>
        public static FiltersetExpressionElement Create(Filterset filterset, IFilter filter, int elementNumber)
        {
            return new FiltersetExpressionElement
            {
                FiltersetId = filterset.Id,
                Filter = filter,
                ElementType = FiltersetExpressionElementType.Filter,
                ElementNumber = elementNumber
            };
        }

        /// <summary>
        /// Gets or sets the filterset identifier.
        /// </summary>
        [Column(Name = "filterset_id", IsPrimaryKey = true)]
        public Guid FiltersetId { get; set; }

        /// <summary>
        /// Gets or sets the filterset element number.
        /// </summary>
        [Column(Name = "element_number", IsPrimaryKey = true)]
        public int ElementNumber { get; set; }

        /// <summary>
        /// Gets or sets the filterset element type.
        /// </summary>
        [Column(Name = "element_type")]
        public FiltersetExpressionElementType ElementType { get; set; }

        /// <summary>
        /// Gets or sets the filter identifier.
        /// </summary>
        [Column(Name = "filter_id")]
        public Guid? FilterId { get; set; }

        /// <summary>
        /// Gets or sets the filter comparison.
        /// </summary>
        [Column(Name = "filter_comparison")]
        public FilterComparison? Comparison { get; set; }

        /// <summary>
        /// Gets or sets the value to filter against.
        /// </summary>
        [Column(Name = "filter_against")]
        public string FilterAgainst { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public IFilter Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                FilterId = _filter?.Id;
            }
        }
        private IFilter _filter;

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        public override string ToString()
        {
            switch (ElementType)
            {
                case FiltersetExpressionElementType.OpenParenthesis:
                    return "(";
                case FiltersetExpressionElementType.CloseParenthesis:
                    return ")";
                case FiltersetExpressionElementType.And:
                    return " and ";
                case FiltersetExpressionElementType.Or:
                    return " or ";
                case FiltersetExpressionElementType.Filter:
                    if (Filter != null && Comparison != null)
                        return $"{Filter.Label} {Comparison} {FilterAgainst}";
                    return FilterAgainst;
                default:
                    return FilterAgainst;
            }
        }
    }
}
