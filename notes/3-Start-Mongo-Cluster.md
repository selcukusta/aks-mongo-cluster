To run a command on any mongo instance;

```bash
kubectl exec -ti mongo-1 -- mongo
```
```bash
rs.initiate({_id: "rs0", version: 1, members: [
       { _id: 0, host : "mongo-0.mongo.default.svc.cluster.local:27017" },
       { _id: 1, host : "mongo-1.mongo.default.svc.cluster.local:27017" },
       { _id: 2, host : "mongo-2.mongo.default.svc.cluster.local:27017" }
 ]});
 ```