#!/bin/bash

git submodule init
git submodule update

pushd MetaphysicsIndustries.Solus
git submodule init
git submodule update
popd

