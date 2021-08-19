/*eslint-disable block-scoped-var, id-length, no-control-regex, no-magic-numbers, no-prototype-builtins, no-redeclare, no-shadow, no-var, sort-vars*/
"use strict";

var $protobuf = require("protobufjs/light");

var $root = ($protobuf.roots["default"] || ($protobuf.roots["default"] = new $protobuf.Root()))
.addJSON({
  ServerProto: {
    nested: {
      BaseMessage: {
        fields: {
          auth: {
            type: "string",
            id: 1
          },
          userid: {
            type: "int32",
            id: 2
          },
          msg_type: {
            type: "int32",
            id: 3
          },
          main_command: {
            type: "int32",
            id: 4
          },
          command: {
            type: "int64",
            id: 5
          },
          content: {
            type: "bytes",
            id: 6
          },
          time: {
            type: "int64",
            id: 7
          }
        }
      },
      Service_Info: {
        fields: {
          serviceId: {
            type: "int64",
            id: 1
          },
          host: {
            type: "string",
            id: 2
          },
          port: {
            type: "int32",
            id: 3
          },
          serviceType: {
            type: "int32",
            id: 4
          },
          socketHost: {
            type: "string",
            id: 5
          },
          socketPort: {
            type: "int32",
            id: 6
          }
        }
      }
    }
  }
});

module.exports = $root;
