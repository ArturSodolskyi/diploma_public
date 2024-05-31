import { NodeType } from "./nodeType";

export class Node {
    id!: number;
    name!: string;
    type!: NodeType;
    children?: Node[];
}
