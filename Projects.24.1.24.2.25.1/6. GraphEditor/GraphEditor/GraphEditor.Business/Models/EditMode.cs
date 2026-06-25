namespace GraphEditor.Business.Models
{
    public enum EditMode
    {
        None,
        Creating,
        Moving,
        // Для прямоугольника и эллипса
        ResizeT,
        ResizeB,
        ResizeL,
        ResizeR,
        ResizeTL,
        ResizeTR,
        ResizeBL,
        ResizeBR,
        // Для треугольника – перемещение вершин
        MoveVertex1,
        MoveVertex2,
        MoveVertex3
    }
}