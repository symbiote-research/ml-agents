FROM nvidia/cuda:10.0-cudnn7-devel-ubuntu18.04

RUN yes | unminimize

RUN echo "deb http://packages.cloud.google.com/apt cloud-sdk-xenial main" | tee -a /etc/apt/sources.list.d/google-cloud-sdk.list
RUN wget https://packages.cloud.google.com/apt/doc/apt-key.gpg && apt-key add apt-key.gpg
RUN apt-get update && \
  apt-get install -y --no-install-recommends wget curl tmux vim git gdebi-core \
  build-essential python3-pip unzip google-cloud-sdk htop mesa-utils xorg-dev xorg \
  libglvnd-dev libgl1-mesa-dev libegl1-mesa-dev libgles2-mesa-dev && \
  wget http://security.ubuntu.com/ubuntu/pool/main/libx/libxfont/libxfont1_1.5.1-1ubuntu0.16.04.4_amd64.deb && \
  wget http://security.ubuntu.com/ubuntu/pool/universe/x/xorg-server/xvfb_1.18.4-0ubuntu0.11_amd64.deb && \
  yes | gdebi libxfont1_1.5.1-1ubuntu0.16.04.4_amd64.deb && \
  yes | gdebi xvfb_1.18.4-0ubuntu0.11_amd64.deb
RUN python3 -m pip install --upgrade pip
RUN pip install setuptools==41.0.0

ENV LD_LIBRARY_PATH=/usr/lib/x86_64-linux-gnu:$LD_LIBRARY_PATH

COPY . /ml-agents
WORKDIR /ml-agents

RUN pip install -e /ml-agents/ml-agents-envs
RUN pip install -e /ml-agents/ml-agents
