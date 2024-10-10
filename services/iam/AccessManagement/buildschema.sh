#!/bin/bash

rm -rf "Data/Migrations"

dotnet ef migrations add Initial -c AppDbContext -o Data/Migrations