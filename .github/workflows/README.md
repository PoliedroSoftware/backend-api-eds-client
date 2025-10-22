# GitHub Actions Workflows for AWS Deployment

This directory contains GitHub Actions workflows for building, testing, and deploying the backend-api-eds-client application to AWS ECS.

## Workflows Overview

### Main Workflow: `aws.yml`
The main workflow that orchestrates the entire CI/CD pipeline:
- **Triggers**: 
  - Push to `main` branch
  - Pull requests to `main`, `release/*`, or `releasecandidate/*` branches
- **Jobs**:
  1. **dotnet**: Build and test the .NET application
  2. **docker**: Build and push Docker images (only on merge or main push)
  3. **deploy-dev**: Deploy to AWS ECS dev environment (only on merge or main push)

### Reusable Workflows

#### `reusable-dotnet-build.yml`
Builds and tests .NET applications.
- Restores NuGet packages with caching
- Builds the solution in Release configuration
- Runs unit tests
- Uploads test results as artifacts

#### `reusable-docker-build.yml`
Builds and pushes Docker images to container registries.
- Supports Docker Hub and/or Amazon ECR
- Multi-platform builds (default: linux/amd64)
- Uses GitHub Actions cache for faster builds
- Configurable image names and tags

#### `reusable-aws-deploy.yml`
Deploys applications to AWS ECS.
- Updates ECS service with new task definition
- Waits for deployment to complete
- Verifies deployment success
- Supports multiple environments (dev, qa, prod)

## Required Configuration

### GitHub Secrets

The following secrets must be configured in your GitHub repository:

#### Docker Hub Secrets
- `DOCKERHUB_USERNAME_CLIENT` - Docker Hub username for authentication
- `DOCKERHUB_TOKEN` - Docker Hub access token (not password)

#### AWS Secrets
- `AWS_ACCESS_KEY_ID` - AWS IAM access key with ECR and ECS permissions
- `AWS_SECRET_ACCESS_KEY` - AWS IAM secret key
- `AWS_ACCOUNT_ID` - Your AWS account ID (12-digit number)
- `ECR_REPOSITORY` - Name of your ECR repository
- `AWS_ECS_CLUSTER` - Name of your ECS cluster
- `AWS_ECS_SERVICE` - Name of your ECS service
- `TASK_DEFINITION_ARN` - ARN of your ECS task definition

### GitHub Variables (Optional)

- `DOCKERHUB_REPOSITORY_CLIENT` - Docker Hub repository name (default: `backend-api-eds-client`)
- `AWS_REGION` - AWS region (default: `us-east-2`)

## How to Configure Secrets

1. Go to your GitHub repository
2. Navigate to Settings → Secrets and variables → Actions
3. Click "New repository secret"
4. Add each secret listed above with its corresponding value

## Workflow Behavior

### On Pull Request
- Runs build and tests only
- Does not build Docker images or deploy

### On Merge to Main (or Direct Push)
- Runs build and tests
- Builds Docker images and pushes to both Docker Hub and ECR
- Deploys to AWS ECS dev environment
- Waits for deployment to complete and verifies success

## Concurrency Control

The workflows use concurrency control to:
- Cancel in-progress runs when a new commit is pushed to the same branch
- Prevent multiple simultaneous deployments

## Customization

### Changing .NET Version
Edit the `dotnet-version` parameter in `aws.yml`:
```yaml
dotnet-version: '8.0.x'  # Change to your desired version
```

### Changing Docker Registry
Edit the `registry-type` parameter in `aws.yml`:
```yaml
registry-type: 'ecr'       # ECR only
registry-type: 'dockerhub' # Docker Hub only
registry-type: 'both'      # Both registries (default)
```

### Adding Additional Environments
Add new deployment jobs in `aws.yml`:
```yaml
deploy-qa:
  if: github.event.pull_request.merged == true
  needs: docker
  uses: ./.github/workflows/reusable-aws-deploy.yml
  with:
    environment: qa
    aws-region: ${{ vars.AWS_REGION || 'us-east-2' }}
  secrets:
    # ... same secrets as deploy-dev
```

## Troubleshooting

### Build Failures
- Check that all NuGet packages are accessible
- Verify .NET version compatibility
- Review test results in the Actions artifacts

### Docker Build Failures
- Ensure Dockerfile is in repository root
- Verify Docker Hub credentials are correct
- Check AWS ECR permissions

### Deployment Failures
- Verify ECS cluster and service names are correct
- Check task definition ARN is valid
- Ensure AWS credentials have necessary ECS permissions
- Review CloudWatch logs for ECS tasks

## Related Documentation

- [GitHub Actions Documentation](https://docs.github.com/en/actions)
- [AWS ECS Documentation](https://docs.aws.amazon.com/ecs/)
- [Docker Hub Documentation](https://docs.docker.com/docker-hub/)
