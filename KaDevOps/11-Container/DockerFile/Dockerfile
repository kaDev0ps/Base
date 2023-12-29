FROM ubuntu
MAINTAINER KaDevOps <faq@kadevops.ru>
RUN apt-get update \
    && apt-get install -y cowsay \
    && ln -s /usr/games/cowsay /usr/bin/cowsay \
    && rm -rf /var/lib/apt/lists/*
ENTRYPOINT ["cowsay"]