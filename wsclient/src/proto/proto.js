/*eslint-disable block-scoped-var, id-length, no-control-regex, no-magic-numbers, no-prototype-builtins, no-redeclare, no-shadow, no-var, sort-vars*/
"use strict";

var $protobuf = require("protobufjs/light");

var $root = ($protobuf.roots["default"] || ($protobuf.roots["default"] = new $protobuf.Root()))
.addJSON({
  ServerProto: {
    nested: {
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
      },
      Socket_Package: {
        fields: {
          main_command: {
            type: "int32",
            id: 1
          },
          command: {
            type: "int64",
            id: 2
          },
          msg_type: {
            type: "int32",
            id: 3
          },
          content: {
            type: "bytes",
            id: 4
          },
          time: {
            type: "int64",
            id: 5
          }
        }
      },
      Server_Package: {
        fields: {
          service_id: {
            type: "int64",
            id: 1
          },
          service_type: {
            type: "int32",
            id: 2
          },
          userid: {
            type: "int64",
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
      TestMessage: {
        fields: {
          msg: {
            type: "string",
            id: 1
          }
        }
      }
    }
  }
});

module.exports = $root;
