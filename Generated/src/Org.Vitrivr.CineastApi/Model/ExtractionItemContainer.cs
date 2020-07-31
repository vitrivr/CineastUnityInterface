/* 
 * Cineast RESTful API
 *
 * Cineast is vitrivr's content-based multimedia retrieval engine. This is it's RESTful API.
 *
 * The version of the OpenAPI document: v1
 * Contact: contact@vitrivr.org
 * Generated by: https://github.com/openapitools/openapi-generator.git
 */

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel.DataAnnotations;
using OpenAPIDateConverter = Org.Vitrivr.CineastApi.Client.OpenAPIDateConverter;

namespace Org.Vitrivr.CineastApi.Model
{
    /// <summary>
    /// ExtractionItemContainer
    /// </summary>
    [DataContract]
    public partial class ExtractionItemContainer :  IEquatable<ExtractionItemContainer>, IValidatableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExtractionItemContainer" /> class.
        /// </summary>
        /// <param name="_object">_object.</param>
        /// <param name="metadata">metadata.</param>
        /// <param name="uri">uri.</param>
        public ExtractionItemContainer(MediaObjectDescriptor _object = default(MediaObjectDescriptor), List<MediaObjectMetadataDescriptor> metadata = default(List<MediaObjectMetadataDescriptor>), string uri = default(string))
        {
            this.Object = _object;
            this.Metadata = metadata;
            this.Uri = uri;
        }
        
        /// <summary>
        /// Gets or Sets Object
        /// </summary>
        [DataMember(Name="object", EmitDefaultValue=false)]
        public MediaObjectDescriptor Object { get; set; }

        /// <summary>
        /// Gets or Sets Metadata
        /// </summary>
        [DataMember(Name="metadata", EmitDefaultValue=false)]
        public List<MediaObjectMetadataDescriptor> Metadata { get; set; }

        /// <summary>
        /// Gets or Sets Uri
        /// </summary>
        [DataMember(Name="uri", EmitDefaultValue=false)]
        public string Uri { get; set; }

        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class ExtractionItemContainer {\n");
            sb.Append("  Object: ").Append(Object).Append("\n");
            sb.Append("  Metadata: ").Append(Metadata).Append("\n");
            sb.Append("  Uri: ").Append(Uri).Append("\n");
            sb.Append("}\n");
            return sb.ToString();
        }
  
        /// <summary>
        /// Returns the JSON string presentation of the object
        /// </summary>
        /// <returns>JSON string presentation of the object</returns>
        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <summary>
        /// Returns true if objects are equal
        /// </summary>
        /// <param name="input">Object to be compared</param>
        /// <returns>Boolean</returns>
        public override bool Equals(object input)
        {
            return this.Equals(input as ExtractionItemContainer);
        }

        /// <summary>
        /// Returns true if ExtractionItemContainer instances are equal
        /// </summary>
        /// <param name="input">Instance of ExtractionItemContainer to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(ExtractionItemContainer input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Object == input.Object ||
                    (this.Object != null &&
                    this.Object.Equals(input.Object))
                ) && 
                (
                    this.Metadata == input.Metadata ||
                    this.Metadata != null &&
                    input.Metadata != null &&
                    this.Metadata.SequenceEqual(input.Metadata)
                ) && 
                (
                    this.Uri == input.Uri ||
                    (this.Uri != null &&
                    this.Uri.Equals(input.Uri))
                );
        }

        /// <summary>
        /// Gets the hash code
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            unchecked // Overflow is fine, just wrap
            {
                int hashCode = 41;
                if (this.Object != null)
                    hashCode = hashCode * 59 + this.Object.GetHashCode();
                if (this.Metadata != null)
                    hashCode = hashCode * 59 + this.Metadata.GetHashCode();
                if (this.Uri != null)
                    hashCode = hashCode * 59 + this.Uri.GetHashCode();
                return hashCode;
            }
        }

        /// <summary>
        /// To validate all properties of the instance
        /// </summary>
        /// <param name="validationContext">Validation context</param>
        /// <returns>Validation Result</returns>
        IEnumerable<System.ComponentModel.DataAnnotations.ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            yield break;
        }
    }

}
