<template>
    <div>
            <p><input class="text" placeholder="用户Token" v-model="token" /></p>
            <p><input class="text" placeholder="socket地址" v-model="socketPath" /></p>
            <p><textarea class="text" style="" rows="5"  v-model="view" /></p>
            <p><input class="text" v-model="txt" /></p>
        <p>
            <button @click="connect">连接</button>
            <button @click="disconnect">断开连接</button>
            <button @click="send">发消息</button>
        </p>
    </div>
</template>

<script>
import re from "../lib/request"
export default {
    data () {
        return {
            socketPath:"ws://127.0.0.1:33334/",
            socket:"",
            txtarea:"test",
            token:"token123",
            txt:"",
            view:""
        }
    },
    mounted () {
        // 初始化
    },
    methods: {
        init: function () {
            if(typeof(WebSocket) === "undefined"){
                alert("您的浏览器不支持socket")
            }else{
                // 实例化socket
                this.socket = new WebSocket(this.socketPath)
                // 监听socket连接
                this.socket.onopen = this.open
                // 监听socket错误信息
                this.socket.onerror = this.error
                // 监听socket消息
                this.socket.onmessage = this.getMessage
            }
        },
        connect: function(){
            this.init()
        },
        disconnect: function(){
            this.socket.close();
            console.log("socket已经关闭")
        },
        open: function () {
            console.log("socket连接成功")
            this.socket.send(this.token)
        },
        error: function () {
            console.log("连接错误")
        },
        getMessage: function (msg) {
            console.log("接收到消息");
            console.log(msg.data);
            var msss = msg.data;
            console.log(typeof msss);
            let reader = new FileReader();
            reader.readAsArrayBuffer(msss);
            reader.onload = function(){
            var buf = new Uint8Array(reader.result);
            console.info(buf);
            let socket_package = re.decode('Socket_Package',buf);
            console.log(socket_package);
            }
        },
        send: function () {
        var text = this.txt;
        var mess = {
            msg : text
        }
        var ts = +new Date();
        var data = {
                main_command:1,
                command:2,
                msg_type:3,
                content:re.encode('TestMessage',mess),
                time:ts
            };
            var msg = re.encode('Socket_Package',data);
            console.log(typeof msg);
            console.log(msg);
            //console.log(msg)
            let socket_package = re.decode('Socket_Package',msg);
            console.log(socket_package);
            this.socket.send(msg)
        },
        close: function () {
            console.log("socket已经关闭")
        }
    },
    unmounted () {
        // 销毁监听
        this.socket.onclose = this.close
    }
}
</script>

<style>

</style>