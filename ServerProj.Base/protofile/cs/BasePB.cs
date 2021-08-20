// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: BasePB.proto
// </auto-generated>
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace ServerProto {

  /// <summary>Holder for reflection information generated from BasePB.proto</summary>
  public static partial class BasePBReflection {

    #region Descriptor
    /// <summary>File descriptor for BasePB.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static BasePBReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "CgxCYXNlUEIucHJvdG8SC1NlcnZlclByb3RvIjwKC1Jlc3VsdF9JbmZvEg4K",
            "BnJlc3VsdBgBIAEoBRIMCgRjb2RlGAIgASgFEg8KB21lc3NhZ2UYAyABKAli",
            "BnByb3RvMw=="));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(null, null, new pbr::GeneratedClrTypeInfo[] {
            new pbr::GeneratedClrTypeInfo(typeof(global::ServerProto.Result_Info), global::ServerProto.Result_Info.Parser, new[]{ "Result", "Code", "Message" }, null, null, null, null)
          }));
    }
    #endregion

  }
  #region Messages
  public sealed partial class Result_Info : pb::IMessage<Result_Info>
  #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      , pb::IBufferMessage
  #endif
  {
    private static readonly pb::MessageParser<Result_Info> _parser = new pb::MessageParser<Result_Info>(() => new Result_Info());
    private pb::UnknownFieldSet _unknownFields;
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pb::MessageParser<Result_Info> Parser { get { return _parser; } }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public static pbr::MessageDescriptor Descriptor {
      get { return global::ServerProto.BasePBReflection.Descriptor.MessageTypes[0]; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    pbr::MessageDescriptor pb::IMessage.Descriptor {
      get { return Descriptor; }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Result_Info() {
      OnConstruction();
    }

    partial void OnConstruction();

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Result_Info(Result_Info other) : this() {
      result_ = other.result_;
      code_ = other.code_;
      message_ = other.message_;
      _unknownFields = pb::UnknownFieldSet.Clone(other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public Result_Info Clone() {
      return new Result_Info(this);
    }

    /// <summary>Field number for the "result" field.</summary>
    public const int ResultFieldNumber = 1;
    private int result_;
    /// <summary>
    ///结果
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Result {
      get { return result_; }
      set {
        result_ = value;
      }
    }

    /// <summary>Field number for the "code" field.</summary>
    public const int CodeFieldNumber = 2;
    private int code_;
    /// <summary>
    ///返回代码
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int Code {
      get { return code_; }
      set {
        code_ = value;
      }
    }

    /// <summary>Field number for the "message" field.</summary>
    public const int MessageFieldNumber = 3;
    private string message_ = "";
    /// <summary>
    ///返回信息
    /// </summary>
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public string Message {
      get { return message_; }
      set {
        message_ = pb::ProtoPreconditions.CheckNotNull(value, "value");
      }
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override bool Equals(object other) {
      return Equals(other as Result_Info);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public bool Equals(Result_Info other) {
      if (ReferenceEquals(other, null)) {
        return false;
      }
      if (ReferenceEquals(other, this)) {
        return true;
      }
      if (Result != other.Result) return false;
      if (Code != other.Code) return false;
      if (Message != other.Message) return false;
      return Equals(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override int GetHashCode() {
      int hash = 1;
      if (Result != 0) hash ^= Result.GetHashCode();
      if (Code != 0) hash ^= Code.GetHashCode();
      if (Message.Length != 0) hash ^= Message.GetHashCode();
      if (_unknownFields != null) {
        hash ^= _unknownFields.GetHashCode();
      }
      return hash;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public override string ToString() {
      return pb::JsonFormatter.ToDiagnosticString(this);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void WriteTo(pb::CodedOutputStream output) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      output.WriteRawMessage(this);
    #else
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Result);
      }
      if (Code != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Code);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(output);
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalWriteTo(ref pb::WriteContext output) {
      if (Result != 0) {
        output.WriteRawTag(8);
        output.WriteInt32(Result);
      }
      if (Code != 0) {
        output.WriteRawTag(16);
        output.WriteInt32(Code);
      }
      if (Message.Length != 0) {
        output.WriteRawTag(26);
        output.WriteString(Message);
      }
      if (_unknownFields != null) {
        _unknownFields.WriteTo(ref output);
      }
    }
    #endif

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public int CalculateSize() {
      int size = 0;
      if (Result != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Result);
      }
      if (Code != 0) {
        size += 1 + pb::CodedOutputStream.ComputeInt32Size(Code);
      }
      if (Message.Length != 0) {
        size += 1 + pb::CodedOutputStream.ComputeStringSize(Message);
      }
      if (_unknownFields != null) {
        size += _unknownFields.CalculateSize();
      }
      return size;
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(Result_Info other) {
      if (other == null) {
        return;
      }
      if (other.Result != 0) {
        Result = other.Result;
      }
      if (other.Code != 0) {
        Code = other.Code;
      }
      if (other.Message.Length != 0) {
        Message = other.Message;
      }
      _unknownFields = pb::UnknownFieldSet.MergeFrom(_unknownFields, other._unknownFields);
    }

    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    public void MergeFrom(pb::CodedInputStream input) {
    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
      input.ReadRawMessage(this);
    #else
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, input);
            break;
          case 8: {
            Result = input.ReadInt32();
            break;
          }
          case 16: {
            Code = input.ReadInt32();
            break;
          }
          case 26: {
            Message = input.ReadString();
            break;
          }
        }
      }
    #endif
    }

    #if !GOOGLE_PROTOBUF_REFSTRUCT_COMPATIBILITY_MODE
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute]
    void pb::IBufferMessage.InternalMergeFrom(ref pb::ParseContext input) {
      uint tag;
      while ((tag = input.ReadTag()) != 0) {
        switch(tag) {
          default:
            _unknownFields = pb::UnknownFieldSet.MergeFieldFrom(_unknownFields, ref input);
            break;
          case 8: {
            Result = input.ReadInt32();
            break;
          }
          case 16: {
            Code = input.ReadInt32();
            break;
          }
          case 26: {
            Message = input.ReadString();
            break;
          }
        }
      }
    }
    #endif

  }

  #endregion

}

#endregion Designer generated code
