import protoBase from "../proto/proto"
const RootPath = 'ServerProto.'
var request = {
    create: function (data) {
        var requestMsg = protoBase.lookup(RootPath + 'BaseMessage')
        return requestMsg.encode(data).finish();
    },
    decreate: function (data) {
        var requestMsg = protoBase.lookup(RootPath + 'BaseMessage')
        return requestMsg.decode(data)
    },
    str: function (txt) {
        return new Blob([txt]);
    },
    encode: function (name, data) {
        var requestMsg = protoBase.lookup(RootPath + name)
        return requestMsg.encode(data).finish();
    },
    decode: function (name, data) {
        var requestMsg = protoBase.lookup(RootPath + name)
        return requestMsg.decode(data)
    },
}
export default request