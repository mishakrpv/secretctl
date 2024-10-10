#!/bin/bash

rm -rf "Data/Migrations"

dotnet ef migrations add Initial -c ApplicationDbContext -o Data/Migrations