export class DrawingDict
{
    constructor()
    {
        this.canvasMap = new Map;
    }

    addCanvas(canvas, ID)
    {
        this.canvasMap.set(ID, canvas);
        
    }

    getCanvas(ID)
    {
        return this.canvasMap.get(ID);
    }
}