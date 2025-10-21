# Docker Health Check Configuration

## Dockerfile Health Check
Add this to your Dockerfile to enable container health monitoring:

```dockerfile
# Add health check to Dockerfile
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health/live || exit 1
```

## Docker Compose Health Check
Add this to your docker-compose.yml:

```yaml
version: '3.8'
services:
  poliedro-client-api:
    build: .
    ports:
      - "5062:8080"
    environment:
      - ASPNETCORE_ENVIRONMENT=Production
 - MYSQL_CONNECTION=${MYSQL_CONNECTION}
    healthcheck:
    test: ["CMD", "curl", "-f", "http://localhost:8080/health/live"]
      interval: 30s
      timeout: 10s
    retries: 3
      start_period: 40s
    depends_on:
      mysql:
        condition: service_healthy
      
  mysql:
  image: mysql:8.0
    environment:
      MYSQL_ROOT_PASSWORD: ${MYSQL_ROOT_PASSWORD}
    MYSQL_DATABASE: ${MYSQL_DATABASE}
    healthcheck:
 test: ["CMD", "mysqladmin", "ping", "-h", "localhost"]
      interval: 10s
      timeout: 5s
      retries: 5
      start_period: 30s
```

## Kubernetes Health Checks

### Deployment.yaml
```yaml
apiVersion: apps/v1
kind: Deployment
metadata:
  name: poliedro-client-api
spec:
  replicas: 3
  selector:
    matchLabels:
    app: poliedro-client-api
  template:
    metadata:
      labels:
        app: poliedro-client-api
    spec:
      containers:
 - name: api
        image: poliedro/client-api:latest
  ports:
    - containerPort: 8080
        env:
        - name: ASPNETCORE_ENVIRONMENT
          value: "Production"
   - name: MYSQL_CONNECTION
          valueFrom:
   secretKeyRef:
  name: mysql-secret
         key: connection-string
        livenessProbe:
 httpGet:
            path: /health/live
            port: 8080
      initialDelaySeconds: 30
    periodSeconds: 10
  timeoutSeconds: 5
        failureThreshold: 3
        readinessProbe:
          httpGet:
        path: /health/ready  
         port: 8080
     initialDelaySeconds: 5
          periodSeconds: 5
 timeoutSeconds: 3
   successThreshold: 1
          failureThreshold: 3
        resources:
          requests:
            memory: "256Mi"
            cpu: "250m"
          limits:
            memory: "512Mi"
            cpu: "500m"
---
apiVersion: v1
kind: Service
metadata:
  name: poliedro-client-api-service
spec:
  selector:
    app: poliedro-client-api
  ports:
  - protocol: TCP
    port: 80
    targetPort: 8080
  type: ClusterIP
```

## Azure Container Apps Health Check

```json
{
  "properties": {
    "configuration": {
      "ingress": {
        "external": true,
     "targetPort": 8080
    }
    },
    "template": {
      "containers": [
      {
      "name": "poliedro-client-api",
          "image": "poliedro/client-api:latest",
          "env": [
            {
       "name": "ASPNETCORE_ENVIRONMENT",
              "value": "Production"
            }
   ],
          "probes": [
          {
   "type": "liveness",
              "httpGet": {
          "path": "/health/live",
                "port": 8080
  },
    "initialDelaySeconds": 30,
              "periodSeconds": 10
   },
     {
   "type": "readiness", 
         "httpGet": {
    "path": "/health/ready",
        "port": 8080
   },
        "initialDelaySeconds": 5,
  "periodSeconds": 5
        }
          ]
    }
      ]
    }
  }
}
```