#!/bin/sh
DIR=`date +%m%d%y`
DEST=../db_backups/$DIR
mongodump -h localhost -d zaynbot -o $DEST