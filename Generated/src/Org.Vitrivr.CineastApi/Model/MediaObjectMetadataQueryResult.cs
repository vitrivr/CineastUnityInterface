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
    /// MediaObjectMetadataQueryResult
    /// </summary>
    [DataContract]
    public partial class MediaObjectMetadataQueryResult :  IEquatable<MediaObjectMetadataQueryResult>, IValidatableObject
    {
        /// <summary>
        /// Defines MessageType
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public enum MessageTypeEnum
        {
            /// <summary>
            /// Enum PING for value: PING
            /// </summary>
            [EnumMember(Value = "PING")]
            PING = 1,

            /// <summary>
            /// Enum QSIM for value: Q_SIM
            /// </summary>
            [EnumMember(Value = "Q_SIM")]
            QSIM = 2,

            /// <summary>
            /// Enum QMLT for value: Q_MLT
            /// </summary>
            [EnumMember(Value = "Q_MLT")]
            QMLT = 3,

            /// <summary>
            /// Enum QNESEG for value: Q_NESEG
            /// </summary>
            [EnumMember(Value = "Q_NESEG")]
            QNESEG = 4,

            /// <summary>
            /// Enum QSEG for value: Q_SEG
            /// </summary>
            [EnumMember(Value = "Q_SEG")]
            QSEG = 5,

            /// <summary>
            /// Enum MLOOKUP for value: M_LOOKUP
            /// </summary>
            [EnumMember(Value = "M_LOOKUP")]
            MLOOKUP = 6,

            /// <summary>
            /// Enum QTEMPORAL for value: Q_TEMPORAL
            /// </summary>
            [EnumMember(Value = "Q_TEMPORAL")]
            QTEMPORAL = 7,

            /// <summary>
            /// Enum SESSIONSTART for value: SESSION_START
            /// </summary>
            [EnumMember(Value = "SESSION_START")]
            SESSIONSTART = 8,

            /// <summary>
            /// Enum QRSTART for value: QR_START
            /// </summary>
            [EnumMember(Value = "QR_START")]
            QRSTART = 9,

            /// <summary>
            /// Enum QREND for value: QR_END
            /// </summary>
            [EnumMember(Value = "QR_END")]
            QREND = 10,

            /// <summary>
            /// Enum QRERROR for value: QR_ERROR
            /// </summary>
            [EnumMember(Value = "QR_ERROR")]
            QRERROR = 11,

            /// <summary>
            /// Enum QROBJECT for value: QR_OBJECT
            /// </summary>
            [EnumMember(Value = "QR_OBJECT")]
            QROBJECT = 12,

            /// <summary>
            /// Enum QRMETADATAO for value: QR_METADATA_O
            /// </summary>
            [EnumMember(Value = "QR_METADATA_O")]
            QRMETADATAO = 13,

            /// <summary>
            /// Enum QRMETADATAS for value: QR_METADATA_S
            /// </summary>
            [EnumMember(Value = "QR_METADATA_S")]
            QRMETADATAS = 14,

            /// <summary>
            /// Enum QRSEGMENT for value: QR_SEGMENT
            /// </summary>
            [EnumMember(Value = "QR_SEGMENT")]
            QRSEGMENT = 15,

            /// <summary>
            /// Enum QRSIMILARITY for value: QR_SIMILARITY
            /// </summary>
            [EnumMember(Value = "QR_SIMILARITY")]
            QRSIMILARITY = 16

        }

        /// <summary>
        /// Gets or Sets MessageType
        /// </summary>
        [DataMember(Name="messageType", EmitDefaultValue=false)]
        public MessageTypeEnum? MessageType { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="MediaObjectMetadataQueryResult" /> class.
        /// </summary>
        /// <param name="content">content.</param>
        /// <param name="queryId">queryId.</param>
        /// <param name="messageType">messageType.</param>
        public MediaObjectMetadataQueryResult(List<MediaObjectMetadataDescriptor> content = default(List<MediaObjectMetadataDescriptor>), string queryId = default(string), MessageTypeEnum? messageType = default(MessageTypeEnum?))
        {
            this.Content = content;
            this.QueryId = queryId;
            this.MessageType = messageType;
        }
        
        /// <summary>
        /// Gets or Sets Content
        /// </summary>
        [DataMember(Name="content", EmitDefaultValue=false)]
        public List<MediaObjectMetadataDescriptor> Content { get; set; }

        /// <summary>
        /// Gets or Sets QueryId
        /// </summary>
        [DataMember(Name="queryId", EmitDefaultValue=false)]
        public string QueryId { get; set; }


        /// <summary>
        /// Returns the string presentation of the object
        /// </summary>
        /// <returns>String presentation of the object</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("class MediaObjectMetadataQueryResult {\n");
            sb.Append("  Content: ").Append(Content).Append("\n");
            sb.Append("  QueryId: ").Append(QueryId).Append("\n");
            sb.Append("  MessageType: ").Append(MessageType).Append("\n");
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
            return this.Equals(input as MediaObjectMetadataQueryResult);
        }

        /// <summary>
        /// Returns true if MediaObjectMetadataQueryResult instances are equal
        /// </summary>
        /// <param name="input">Instance of MediaObjectMetadataQueryResult to be compared</param>
        /// <returns>Boolean</returns>
        public bool Equals(MediaObjectMetadataQueryResult input)
        {
            if (input == null)
                return false;

            return 
                (
                    this.Content == input.Content ||
                    this.Content != null &&
                    input.Content != null &&
                    this.Content.SequenceEqual(input.Content)
                ) && 
                (
                    this.QueryId == input.QueryId ||
                    (this.QueryId != null &&
                    this.QueryId.Equals(input.QueryId))
                ) && 
                (
                    this.MessageType == input.MessageType ||
                    (this.MessageType != null &&
                    this.MessageType.Equals(input.MessageType))
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
                if (this.Content != null)
                    hashCode = hashCode * 59 + this.Content.GetHashCode();
                if (this.QueryId != null)
                    hashCode = hashCode * 59 + this.QueryId.GetHashCode();
                if (this.MessageType != null)
                    hashCode = hashCode * 59 + this.MessageType.GetHashCode();
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
