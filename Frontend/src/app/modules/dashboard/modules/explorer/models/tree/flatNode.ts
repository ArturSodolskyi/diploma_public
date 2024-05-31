import { Node } from './node'

export class FlatNode extends Node {
    level!: number;
    icon!: string; 
    expandable!: boolean;
    new!: boolean;
    editable!: boolean;
}