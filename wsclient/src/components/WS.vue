<template>
    <div>
        <span>
            <textarea class="text" v-model="txtarea" />   
        </span>
        <p>
            <button @click="send">发消息</button>
        </p>
    </div>
</template>

<script>
import re from "../lib/request"
export default {
    data () {
        return {
            path:"ws://127.0.0.1:33334/",
            socket:"",
            txtarea:"test"
        }
    },
    mounted () {
        // 初始化
        this.init()
    },
    methods: {
        init: function () {
            if(typeof(WebSocket) === "undefined"){
                alert("您的浏览器不支持socket")
            }else{
                // 实例化socket
                this.socket = new WebSocket(this.path)
                // 监听socket连接
                this.socket.onopen = this.open
                // 监听socket错误信息
                this.socket.onerror = this.error
                // 监听socket消息
                this.socket.onmessage = this.getMessage
            }
        },
        open: function () {
            console.log("socket连接成功")
        },
        error: function () {
            console.log("连接错误")
        },
        getMessage: function (msg) {
            console.log(msg.data)
        },
        send: function () {
        var txt = this.txtarea;
        var text = txt;
        for(var i=1;i<100;i++){
            txt = text+txt;
        }
        var serInfo = {
                serviceId:1,
                host:txt,
                port:1,
                serviceType:1,
                socketType:'test',
                socketPort:2
        };
        console.log(txt);
        var data = {
                auth:"123",
                userid:123456,
                msg_type:1,
                main_command:2,
                command:3,
                content:re.encode('Service_Info',serInfo),
                time:4
            };

            var msg = re.encode('BaseMessage',data);
            console.log(msg)
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