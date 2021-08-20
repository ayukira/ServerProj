// <auto-generated>
//     Generated by the protocol buffer compiler.  DO NOT EDIT!
//     source: ServicePB.proto
// </auto-generated>
#pragma warning disable 0414, 1591
#region Designer generated code

using grpc = global::Grpc.Core;

namespace ServerProto {
  public static partial class RegistryGrpc
  {
    static readonly string __ServiceName = "ServerProto.RegistryGrpc";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::ServerProto.RegistryService_Request> __Marshaller_ServerProto_RegistryService_Request = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.RegistryService_Request.Parser));
    static readonly grpc::Marshaller<global::ServerProto.RegistryService_Response> __Marshaller_ServerProto_RegistryService_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.RegistryService_Response.Parser));
    static readonly grpc::Marshaller<global::ServerProto.UnRegistryService_Request> __Marshaller_ServerProto_UnRegistryService_Request = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.UnRegistryService_Request.Parser));
    static readonly grpc::Marshaller<global::ServerProto.UnRegistryService_Response> __Marshaller_ServerProto_UnRegistryService_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.UnRegistryService_Response.Parser));
    static readonly grpc::Marshaller<global::ServerProto.Trick_Request> __Marshaller_ServerProto_Trick_Request = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.Trick_Request.Parser));
    static readonly grpc::Marshaller<global::ServerProto.Trick_Response> __Marshaller_ServerProto_Trick_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.Trick_Response.Parser));
    static readonly grpc::Marshaller<global::ServerProto.Connect_Request> __Marshaller_ServerProto_Connect_Request = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.Connect_Request.Parser));
    static readonly grpc::Marshaller<global::ServerProto.Connect_Response> __Marshaller_ServerProto_Connect_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.Connect_Response.Parser));
    static readonly grpc::Marshaller<global::ServerProto.ServiceList_Request> __Marshaller_ServerProto_ServiceList_Request = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.ServiceList_Request.Parser));
    static readonly grpc::Marshaller<global::ServerProto.ServiceList_Response> __Marshaller_ServerProto_ServiceList_Response = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.ServiceList_Response.Parser));

    static readonly grpc::Method<global::ServerProto.RegistryService_Request, global::ServerProto.RegistryService_Response> __Method_Registry = new grpc::Method<global::ServerProto.RegistryService_Request, global::ServerProto.RegistryService_Response>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Registry",
        __Marshaller_ServerProto_RegistryService_Request,
        __Marshaller_ServerProto_RegistryService_Response);

    static readonly grpc::Method<global::ServerProto.UnRegistryService_Request, global::ServerProto.UnRegistryService_Response> __Method_UnRegistry = new grpc::Method<global::ServerProto.UnRegistryService_Request, global::ServerProto.UnRegistryService_Response>(
        grpc::MethodType.Unary,
        __ServiceName,
        "UnRegistry",
        __Marshaller_ServerProto_UnRegistryService_Request,
        __Marshaller_ServerProto_UnRegistryService_Response);

    static readonly grpc::Method<global::ServerProto.Trick_Request, global::ServerProto.Trick_Response> __Method_Trick = new grpc::Method<global::ServerProto.Trick_Request, global::ServerProto.Trick_Response>(
        grpc::MethodType.Unary,
        __ServiceName,
        "Trick",
        __Marshaller_ServerProto_Trick_Request,
        __Marshaller_ServerProto_Trick_Response);

    static readonly grpc::Method<global::ServerProto.Connect_Request, global::ServerProto.Connect_Response> __Method_ConnectRegistry = new grpc::Method<global::ServerProto.Connect_Request, global::ServerProto.Connect_Response>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "ConnectRegistry",
        __Marshaller_ServerProto_Connect_Request,
        __Marshaller_ServerProto_Connect_Response);

    static readonly grpc::Method<global::ServerProto.ServiceList_Request, global::ServerProto.ServiceList_Response> __Method_ServiceList = new grpc::Method<global::ServerProto.ServiceList_Request, global::ServerProto.ServiceList_Response>(
        grpc::MethodType.Unary,
        __ServiceName,
        "ServiceList",
        __Marshaller_ServerProto_ServiceList_Request,
        __Marshaller_ServerProto_ServiceList_Response);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ServerProto.ServicePBReflection.Descriptor.Services[0]; }
    }

    /// <summary>Base class for server-side implementations of RegistryGrpc</summary>
    [grpc::BindServiceMethod(typeof(RegistryGrpc), "BindService")]
    public abstract partial class RegistryGrpcBase
    {
      /// <summary>
      ///注册
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::ServerProto.RegistryService_Response> Registry(global::ServerProto.RegistryService_Request request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///注销
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::ServerProto.UnRegistryService_Response> UnRegistry(global::ServerProto.UnRegistryService_Request request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///心跳
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::ServerProto.Trick_Response> Trick(global::ServerProto.Trick_Request request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///rpc Connect (stream Connect_Request) returns (stream Connect_Response);             //通信
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="responseStream">Used for sending responses back to the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>A task indicating completion of the handler.</returns>
      public virtual global::System.Threading.Tasks.Task ConnectRegistry(global::ServerProto.Connect_Request request, grpc::IServerStreamWriter<global::ServerProto.Connect_Response> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      /// <summary>
      ///获取服务器列表
      /// </summary>
      /// <param name="request">The request received from the client.</param>
      /// <param name="context">The context of the server-side call handler being invoked.</param>
      /// <returns>The response to send back to the client (wrapped by a task).</returns>
      public virtual global::System.Threading.Tasks.Task<global::ServerProto.ServiceList_Response> ServiceList(global::ServerProto.ServiceList_Request request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for RegistryGrpc</summary>
    public partial class RegistryGrpcClient : grpc::ClientBase<RegistryGrpcClient>
    {
      /// <summary>Creates a new client for RegistryGrpc</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public RegistryGrpcClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for RegistryGrpc that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public RegistryGrpcClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected RegistryGrpcClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected RegistryGrpcClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      /// <summary>
      ///注册
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.RegistryService_Response Registry(global::ServerProto.RegistryService_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Registry(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///注册
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.RegistryService_Response Registry(global::ServerProto.RegistryService_Request request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Registry, null, options, request);
      }
      /// <summary>
      ///注册
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.RegistryService_Response> RegistryAsync(global::ServerProto.RegistryService_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return RegistryAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///注册
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.RegistryService_Response> RegistryAsync(global::ServerProto.RegistryService_Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Registry, null, options, request);
      }
      /// <summary>
      ///注销
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.UnRegistryService_Response UnRegistry(global::ServerProto.UnRegistryService_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UnRegistry(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///注销
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.UnRegistryService_Response UnRegistry(global::ServerProto.UnRegistryService_Request request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_UnRegistry, null, options, request);
      }
      /// <summary>
      ///注销
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.UnRegistryService_Response> UnRegistryAsync(global::ServerProto.UnRegistryService_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return UnRegistryAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///注销
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.UnRegistryService_Response> UnRegistryAsync(global::ServerProto.UnRegistryService_Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_UnRegistry, null, options, request);
      }
      /// <summary>
      ///心跳
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.Trick_Response Trick(global::ServerProto.Trick_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return Trick(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///心跳
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.Trick_Response Trick(global::ServerProto.Trick_Request request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_Trick, null, options, request);
      }
      /// <summary>
      ///心跳
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.Trick_Response> TrickAsync(global::ServerProto.Trick_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return TrickAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///心跳
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.Trick_Response> TrickAsync(global::ServerProto.Trick_Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_Trick, null, options, request);
      }
      /// <summary>
      ///rpc Connect (stream Connect_Request) returns (stream Connect_Response);             //通信
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::ServerProto.Connect_Response> ConnectRegistry(global::ServerProto.Connect_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ConnectRegistry(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///rpc Connect (stream Connect_Request) returns (stream Connect_Response);             //通信
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncServerStreamingCall<global::ServerProto.Connect_Response> ConnectRegistry(global::ServerProto.Connect_Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_ConnectRegistry, null, options, request);
      }
      /// <summary>
      ///获取服务器列表
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.ServiceList_Response ServiceList(global::ServerProto.ServiceList_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ServiceList(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///获取服务器列表
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The response received from the server.</returns>
      public virtual global::ServerProto.ServiceList_Response ServiceList(global::ServerProto.ServiceList_Request request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_ServiceList, null, options, request);
      }
      /// <summary>
      ///获取服务器列表
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="headers">The initial metadata to send with the call. This parameter is optional.</param>
      /// <param name="deadline">An optional deadline for the call. The call will be cancelled if deadline is hit.</param>
      /// <param name="cancellationToken">An optional token for canceling the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.ServiceList_Response> ServiceListAsync(global::ServerProto.ServiceList_Request request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return ServiceListAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      /// <summary>
      ///获取服务器列表
      /// </summary>
      /// <param name="request">The request to send to the server.</param>
      /// <param name="options">The options for the call.</param>
      /// <returns>The call object.</returns>
      public virtual grpc::AsyncUnaryCall<global::ServerProto.ServiceList_Response> ServiceListAsync(global::ServerProto.ServiceList_Request request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_ServiceList, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override RegistryGrpcClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new RegistryGrpcClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(RegistryGrpcBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_Registry, serviceImpl.Registry)
          .AddMethod(__Method_UnRegistry, serviceImpl.UnRegistry)
          .AddMethod(__Method_Trick, serviceImpl.Trick)
          .AddMethod(__Method_ConnectRegistry, serviceImpl.ConnectRegistry)
          .AddMethod(__Method_ServiceList, serviceImpl.ServiceList).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, RegistryGrpcBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_Registry, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ServerProto.RegistryService_Request, global::ServerProto.RegistryService_Response>(serviceImpl.Registry));
      serviceBinder.AddMethod(__Method_UnRegistry, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ServerProto.UnRegistryService_Request, global::ServerProto.UnRegistryService_Response>(serviceImpl.UnRegistry));
      serviceBinder.AddMethod(__Method_Trick, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ServerProto.Trick_Request, global::ServerProto.Trick_Response>(serviceImpl.Trick));
      serviceBinder.AddMethod(__Method_ConnectRegistry, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::ServerProto.Connect_Request, global::ServerProto.Connect_Response>(serviceImpl.ConnectRegistry));
      serviceBinder.AddMethod(__Method_ServiceList, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ServerProto.ServiceList_Request, global::ServerProto.ServiceList_Response>(serviceImpl.ServiceList));
    }

  }
  public static partial class ServiceGrpc
  {
    static readonly string __ServiceName = "ServerProto.ServiceGrpc";

    static void __Helper_SerializeMessage(global::Google.Protobuf.IMessage message, grpc::SerializationContext context)
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (message is global::Google.Protobuf.IBufferMessage)
      {
        context.SetPayloadLength(message.CalculateSize());
        global::Google.Protobuf.MessageExtensions.WriteTo(message, context.GetBufferWriter());
        context.Complete();
        return;
      }
      #endif
      context.Complete(global::Google.Protobuf.MessageExtensions.ToByteArray(message));
    }

    static class __Helper_MessageCache<T>
    {
      public static readonly bool IsBufferMessage = global::System.Reflection.IntrospectionExtensions.GetTypeInfo(typeof(global::Google.Protobuf.IBufferMessage)).IsAssignableFrom(typeof(T));
    }

    static T __Helper_DeserializeMessage<T>(grpc::DeserializationContext context, global::Google.Protobuf.MessageParser<T> parser) where T : global::Google.Protobuf.IMessage<T>
    {
      #if !GRPC_DISABLE_PROTOBUF_BUFFER_SERIALIZATION
      if (__Helper_MessageCache<T>.IsBufferMessage)
      {
        return parser.ParseFrom(context.PayloadAsReadOnlySequence());
      }
      #endif
      return parser.ParseFrom(context.PayloadAsNewBuffer());
    }

    static readonly grpc::Marshaller<global::ServerProto.Server_Package> __Marshaller_ServerProto_Server_Package = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.Server_Package.Parser));
    static readonly grpc::Marshaller<global::ServerProto.Service_Info> __Marshaller_ServerProto_Service_Info = grpc::Marshallers.Create(__Helper_SerializeMessage, context => __Helper_DeserializeMessage(context, global::ServerProto.Service_Info.Parser));

    static readonly grpc::Method<global::ServerProto.Server_Package, global::ServerProto.Server_Package> __Method_CallMessage = new grpc::Method<global::ServerProto.Server_Package, global::ServerProto.Server_Package>(
        grpc::MethodType.Unary,
        __ServiceName,
        "CallMessage",
        __Marshaller_ServerProto_Server_Package,
        __Marshaller_ServerProto_Server_Package);

    static readonly grpc::Method<global::ServerProto.Service_Info, global::ServerProto.Server_Package> __Method_PushMessage = new grpc::Method<global::ServerProto.Service_Info, global::ServerProto.Server_Package>(
        grpc::MethodType.ServerStreaming,
        __ServiceName,
        "PushMessage",
        __Marshaller_ServerProto_Service_Info,
        __Marshaller_ServerProto_Server_Package);

    /// <summary>Service descriptor</summary>
    public static global::Google.Protobuf.Reflection.ServiceDescriptor Descriptor
    {
      get { return global::ServerProto.ServicePBReflection.Descriptor.Services[1]; }
    }

    /// <summary>Base class for server-side implementations of ServiceGrpc</summary>
    [grpc::BindServiceMethod(typeof(ServiceGrpc), "BindService")]
    public abstract partial class ServiceGrpcBase
    {
      public virtual global::System.Threading.Tasks.Task<global::ServerProto.Server_Package> CallMessage(global::ServerProto.Server_Package request, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

      public virtual global::System.Threading.Tasks.Task PushMessage(global::ServerProto.Service_Info request, grpc::IServerStreamWriter<global::ServerProto.Server_Package> responseStream, grpc::ServerCallContext context)
      {
        throw new grpc::RpcException(new grpc::Status(grpc::StatusCode.Unimplemented, ""));
      }

    }

    /// <summary>Client for ServiceGrpc</summary>
    public partial class ServiceGrpcClient : grpc::ClientBase<ServiceGrpcClient>
    {
      /// <summary>Creates a new client for ServiceGrpc</summary>
      /// <param name="channel">The channel to use to make remote calls.</param>
      public ServiceGrpcClient(grpc::ChannelBase channel) : base(channel)
      {
      }
      /// <summary>Creates a new client for ServiceGrpc that uses a custom <c>CallInvoker</c>.</summary>
      /// <param name="callInvoker">The callInvoker to use to make remote calls.</param>
      public ServiceGrpcClient(grpc::CallInvoker callInvoker) : base(callInvoker)
      {
      }
      /// <summary>Protected parameterless constructor to allow creation of test doubles.</summary>
      protected ServiceGrpcClient() : base()
      {
      }
      /// <summary>Protected constructor to allow creation of configured clients.</summary>
      /// <param name="configuration">The client configuration.</param>
      protected ServiceGrpcClient(ClientBaseConfiguration configuration) : base(configuration)
      {
      }

      public virtual global::ServerProto.Server_Package CallMessage(global::ServerProto.Server_Package request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CallMessage(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual global::ServerProto.Server_Package CallMessage(global::ServerProto.Server_Package request, grpc::CallOptions options)
      {
        return CallInvoker.BlockingUnaryCall(__Method_CallMessage, null, options, request);
      }
      public virtual grpc::AsyncUnaryCall<global::ServerProto.Server_Package> CallMessageAsync(global::ServerProto.Server_Package request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return CallMessageAsync(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncUnaryCall<global::ServerProto.Server_Package> CallMessageAsync(global::ServerProto.Server_Package request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncUnaryCall(__Method_CallMessage, null, options, request);
      }
      public virtual grpc::AsyncServerStreamingCall<global::ServerProto.Server_Package> PushMessage(global::ServerProto.Service_Info request, grpc::Metadata headers = null, global::System.DateTime? deadline = null, global::System.Threading.CancellationToken cancellationToken = default(global::System.Threading.CancellationToken))
      {
        return PushMessage(request, new grpc::CallOptions(headers, deadline, cancellationToken));
      }
      public virtual grpc::AsyncServerStreamingCall<global::ServerProto.Server_Package> PushMessage(global::ServerProto.Service_Info request, grpc::CallOptions options)
      {
        return CallInvoker.AsyncServerStreamingCall(__Method_PushMessage, null, options, request);
      }
      /// <summary>Creates a new instance of client from given <c>ClientBaseConfiguration</c>.</summary>
      protected override ServiceGrpcClient NewInstance(ClientBaseConfiguration configuration)
      {
        return new ServiceGrpcClient(configuration);
      }
    }

    /// <summary>Creates service definition that can be registered with a server</summary>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static grpc::ServerServiceDefinition BindService(ServiceGrpcBase serviceImpl)
    {
      return grpc::ServerServiceDefinition.CreateBuilder()
          .AddMethod(__Method_CallMessage, serviceImpl.CallMessage)
          .AddMethod(__Method_PushMessage, serviceImpl.PushMessage).Build();
    }

    /// <summary>Register service method with a service binder with or without implementation. Useful when customizing the  service binding logic.
    /// Note: this method is part of an experimental API that can change or be removed without any prior notice.</summary>
    /// <param name="serviceBinder">Service methods will be bound by calling <c>AddMethod</c> on this object.</param>
    /// <param name="serviceImpl">An object implementing the server-side handling logic.</param>
    public static void BindService(grpc::ServiceBinderBase serviceBinder, ServiceGrpcBase serviceImpl)
    {
      serviceBinder.AddMethod(__Method_CallMessage, serviceImpl == null ? null : new grpc::UnaryServerMethod<global::ServerProto.Server_Package, global::ServerProto.Server_Package>(serviceImpl.CallMessage));
      serviceBinder.AddMethod(__Method_PushMessage, serviceImpl == null ? null : new grpc::ServerStreamingServerMethod<global::ServerProto.Service_Info, global::ServerProto.Server_Package>(serviceImpl.PushMessage));
    }

  }
}
#endregion
