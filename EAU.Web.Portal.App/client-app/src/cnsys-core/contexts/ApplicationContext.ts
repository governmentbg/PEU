import * as coll from 'typescript-collections';
import { Module } from './BaseModuleContext';

export const allModules: coll.Dictionary<string, Module> = new coll.Dictionary<string, Module>();