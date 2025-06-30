# Start of Selection
FROM mcr.microsoft.com/dotnet/sdk:8.0

# Create the vscode user first
ARG USERNAME=vscode
ARG USER_UID=1000
ARG USER_GID=$USER_UID

# Avoid warnings by switching to noninteractive
ENV DEBIAN_FRONTEND=noninteractive

# Configure apt and install packages
RUN apt-get update \
    && apt-get -y install --no-install-recommends apt-utils dialog 2>&1 \
    && apt-get -y install git procps lsb-release sudo \
    && apt-get autoremove -y \
    && apt-get clean -y \
    && rm -rf /var/lib/apt/lists/*

# Create the vscode user
RUN groupadd --gid $USER_GID $USERNAME \
    && useradd --uid $USER_UID --gid $USER_GID -m $USERNAME \
    && echo $USERNAME ALL=\(root\) NOPASSWD:ALL > /etc/sudoers.d/$USERNAME \
    && chmod 0440 /etc/sudoers.d/$USERNAME

# Switch back to dialog for any ad-hoc use of apt-get
ENV DEBIAN_FRONTEND=dialog

# Set the default shell to bash rather than sh
ENV SHELL /bin/bash

# Set up the workspace directory
WORKDIR /workspace
RUN chown -R $USERNAME:$USERNAME /workspace

# Copy the project files
COPY --chown=$USERNAME:$USERNAME . /workspace/employee-front-end/

# Install dotnet workload as root
RUN dotnet workload install wasm-tools wasm-experimental

# Switch to the vscode user
USER $USERNAME

# Set working directory to the project directory
WORKDIR /workspace/employee-front-end

# Configure Git
RUN git config --global --add safe.directory /workspace \
    && git config --global --add safe.directory /workspace/employee-front-end \
    && git config --global init.defaultBranch main

# Install dotnet-ef tool globally
RUN dotnet tool install --global dotnet-ef

# Generate a self-signed HTTPS certificate for development
RUN dotnet dev-certs https --trust

# Ensure proper ownership of the project directory
RUN sudo chown -R vscode:vscode /workspace/employee-front-end
RUN cd /workspace/employee-front-end && dotnet dev-certs https
# Expose the ports
EXPOSE 7212 5241