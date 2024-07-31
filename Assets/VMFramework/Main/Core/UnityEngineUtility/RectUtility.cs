using UnityEngine;

namespace VMFramework.Core
{
  public static class RectUtility
  {
    public static Rect TakeFromLeft(ref this Rect rect, float width)
    {
      float num = rect.width.Min(width);
      Rect fromLeft = rect;
      fromLeft.width = num;
      rect.x += num;
      rect.width -= num;
      return fromLeft;
    }

    public static Rect TakeFromRight(ref this Rect rect, float width)
    {
      float width1 = rect.width.Min(width);
      Rect fromRight = rect.AlignRight(width1);
      rect.width -= width1;
      return fromRight;
    }

    public static Rect TakeFromTop(ref this Rect rect, float height)
    {
      float num = rect.height.Min(height);
      Rect fromTop = rect;
      fromTop.height = num;
      rect.y += num;
      rect.height -= num;
      return fromTop;
    }

    public static Rect TakeFromBottom(ref this Rect rect, float height)
    {
      float height1 = rect.height.Min(height);
      Rect fromBottom = rect.AlignBottom(height1);
      rect.height -= height;
      return fromBottom;
    }
    
    /// <summary>Returns a Rect with the specified width.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    public static Rect SetWidth(this Rect rect, float width)
    {
      rect.width = width;
      return rect;
    }

    /// <summary>Returns a Rect with the specified height.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect SetHeight(this Rect rect, float height)
    {
      rect.height = height;
      return rect;
    }

    /// <summary>Returns a Rect with the specified size.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect SetSize(this Rect rect, float width, float height)
    {
      rect.width = width;
      rect.height = height;
      return rect;
    }

    /// <summary>Returns a Rect with the specified size.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="widthAndHeight">The desired width and height of the new Rect.</param>
    public static Rect SetSize(this Rect rect, float widthAndHeight)
    {
      rect.width = widthAndHeight;
      rect.height = widthAndHeight;
      return rect;
    }

    /// <summary>Returns a Rect with the specified size.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="size">The desired size of the new Rect.</param>
    public static Rect SetSize(this Rect rect, Vector2 size)
    {
      rect.size = size;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been inserted by the specified amount on the X-axis.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="padding">The desired padding.</param>
    public static Rect HorizontalPadding(this Rect rect, float padding)
    {
      rect.x += padding;
      rect.width -= padding * 2f;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been inserted by the specified amount on the X-axis.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="left">Desired padding on the left side.</param>
    /// <param name="right">Desired padding on the right side.</param>
    public static Rect HorizontalPadding(this Rect rect, float left, float right)
    {
      rect.x += left;
      rect.width -= left + right;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been inserted by the specified amount on the Y-axis.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="padding">The desired padding.</param>
    public static Rect VerticalPadding(this Rect rect, float padding)
    {
      rect.y += padding;
      rect.height -= padding * 2f;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been inserted by the specified amount on the Y-axis.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="top">The desired padding on the top.</param>
    /// <param name="bottom">The desired padding on the bottom.</param>
    public static Rect VerticalPadding(this Rect rect, float top, float bottom)
    {
      rect.y += top;
      rect.height -= top + bottom;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been inserted by the specified amount.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="padding">The desired padding.</param>
    public static Rect Padding(this Rect rect, float padding)
    {
      rect.position += new Vector2(padding, padding);
      rect.size -= new Vector2(padding, padding) * 2f;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been inserted by the specified amount.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="horizontal">The desired horizontal padding.</param>
    /// <param name="vertical">The desired vertical padding.</param>
    public static Rect Padding(this Rect rect, float horizontal, float vertical)
    {
      rect.position += new Vector2(horizontal, vertical);
      rect.size -= new Vector2(horizontal, vertical) * 2f;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been inserted by the specified amount.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="left">The desired padding on the left.</param>
    /// <param name="right">The desired padding on the right.</param>
    /// <param name="top">The desired padding on the top.</param>
    /// <param name="bottom">The desired padding on the bottom.</param>
    public static Rect Padding(this Rect rect, float left, float right, float top, float bottom)
    {
      rect.position += new Vector2(left, top);
      rect.size -= new Vector2(left + right, top + bottom);
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width, that has been aligned to the left of the original Rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    public static Rect AlignLeft(this Rect rect, float width)
    {
      rect.width = width;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width, that has been aligned to horizontal center of the original Rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    public static Rect AlignCenter(this Rect rect, float width)
    {
      rect.x = (float)(rect.x + rect.width * 0.5 - width * 0.5);
      rect.width = width;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width and height in the center of the provided rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect AlignCenter(this Rect rect, float width, float height)
    {
      rect.x = (float)(rect.x + rect.width * 0.5 - width * 0.5);
      rect.y = (float)(rect.y + rect.height * 0.5 - height * 0.5);
      rect.width = width;
      rect.height = height;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width, that has been aligned to the right of the original Rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    public static Rect AlignRight(this Rect rect, float width)
    {
      rect.x = rect.x + rect.width - width;
      rect.width = width;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width, that has been aligned to the right of the original Rect.
    /// </summary>
    public static Rect AlignRight(this Rect rect, float width, bool clamp)
    {
      if (clamp)
      {
        rect.xMin = Mathf.Max(rect.xMax - width, rect.xMin);
        return rect;
      }

      rect.x = rect.x + rect.width - width;
      rect.width = width;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified height, that has been aligned to the top of the original Rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect AlignTop(this Rect rect, float height)
    {
      rect.height = height;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified height, that has been aligned to the vertical middle of the original Rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect AlignMiddle(this Rect rect, float height)
    {
      rect.y = (float)(rect.y + rect.height * 0.5 - height * 0.5);
      rect.height = height;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified height, that has been aligned to the bottom of the original Rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect AlignBottom(this Rect rect, float height)
    {
      rect.y = rect.y + rect.height - height;
      rect.height = height;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width, that has been aligned horizontally to the center of the original rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    public static Rect AlignCenterX(this Rect rect, float width)
    {
      var x = (float)(rect.x + rect.width * 0.5 - width * 0.5);
      return new Rect(x, rect.y, width, rect.height);
    }

    /// <summary>
    /// Returns a Rect, with the specified height, that has been aligned vertically to the center of the original rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect AlignCenterY(this Rect rect, float height)
    {
      rect.y = (float)(rect.y + rect.height * 0.5 - height * 0.5);
      rect.height = height;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width and height, that has been aligned horizontally and vertically to the center of the original rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="size">The desired width and height of the new Rect.</param>
    public static Rect AlignCenterXY(this Rect rect, float size)
    {
      rect.y = (float)(rect.y + rect.height * 0.5 - size * 0.5);
      rect.x = (float)(rect.x + rect.width * 0.5 - size * 0.5);
      rect.height = size;
      rect.width = size;
      return rect;
    }

    /// <summary>
    /// Returns a Rect, with the specified width and height, that has been aligned horizontally and vertically to the center of the original rect.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="width">The desired width of the new Rect.</param>
    /// <param name="height">The desired height of the new Rect.</param>
    public static Rect AlignCenterXY(this Rect rect, float width, float height)
    {
      rect.y = (float)(rect.y + rect.height * 0.5 - height * 0.5);
      rect.x = (float)(rect.x + rect.width * 0.5 - width * 0.5);
      rect.width = width;
      rect.height = height;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been expanded by the specified amount.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="expand">The desired expansion.</param>
    public static Rect Expand(this Rect rect, float expand)
    {
      rect.x -= expand;
      rect.y -= expand;
      rect.height += expand * 2f;
      rect.width += expand * 2f;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been expanded by the specified amount.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="horizontal">The desired expansion on the X-axis.</param>
    /// <param name="vertical">The desired expansion on the Y-axis.</param>
    public static Rect Expand(this Rect rect, float horizontal, float vertical)
    {
      rect.position -= new Vector2(horizontal, vertical);
      rect.size += new Vector2(horizontal, vertical) * 2f;
      return rect;
    }

    /// <summary>
    /// Returns a Rect that has been expanded by the specified amount.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="left">The desired expansion on the left.</param>
    /// <param name="right">The desired expansion on the right.</param>
    /// <param name="top">The desired expansion on the top.</param>
    /// <param name="bottom">The desired expansion on the bottom.</param>
    public static Rect Expand(this Rect rect, float left, float right, float top, float bottom)
    {
      rect.position -= new Vector2(left, top);
      rect.size += new Vector2(left + right, top + bottom);
      return rect;
    }

    /// <summary>
    /// Splits a Rect horizontally into the specified number of sub-rects, and returns a sub-rect for the specified index.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="index">The index for the subrect. Includes 0, and excludes count.</param>
    /// <param name="count">The amount of subrwects the Rect should be split into.</param>
    public static Rect Split(this Rect rect, int index, int count)
    {
      int width = (int)rect.width;
      int num1 = width / count;
      int num2 = width - num1 * count;
      float num3 = rect.x + num1 * index;
      float num4;
      if (index < num2)
      {
        num4 = num3 + index;
        ++num1;
      }
      else
        num4 = num3 + num2;

      rect.x = num4;
      rect.width = num1;
      return rect;
    }

    /// <summary>
    /// Splits a Rect vertically into the specified number of sub-rects, and returns a sub-rect for the specified index.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="index">The index for the subrect. Includes 0, and excludes count.</param>
    /// <param name="count">The amount of subrects the Rect should be split into.</param>
    public static Rect SplitVertical(this Rect rect, int index, int count)
    {
      float num = rect.height / count;
      rect.height = num;
      rect.y += num * index;
      return rect;
    }

    /// <summary>
    /// Splits a Rect into a grid from left to right and then down.
    /// </summary>
    /// <param name="rect">The original rect.</param>
    /// <param name="width">The width of a grid cell.</param>
    /// <param name="height">The height of a grid cell.</param>
    /// <param name="index">The index of the grid cell.</param>
    /// <returns></returns>
    public static Rect SplitGrid(this Rect rect, float width, float height, int index)
    {
      int num1 = (int)(rect.width / (double)width);
      int num2 = num1 > 0 ? num1 : 1;
      int num3 = index % num2;
      int num4 = index / num2;
      rect.x += num3 * width;
      rect.y += num4 * height;
      rect.width = width;
      rect.height = height;
      return rect;
    }

    /// <summary>
    /// Splits a Rect into a grid from left to right and then down.
    /// </summary>
    public static Rect SplitTableGrid(this Rect rect, int columnCount, float rowHeight, int index)
    {
      int num1 = index % columnCount;
      int num2 = index / columnCount;
      float num3 = rect.width / columnCount;
      rect.x += num1 * num3;
      rect.y += num2 * rowHeight;
      rect.width = num3;
      rect.height = rowHeight;
      return rect;
    }

    /// <summary>Moves a Rect to the specified center X position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="x">The desired center x position.</param>
    public static Rect SetCenterX(this Rect rect, float x)
    {
      rect.center = new Vector2(x, rect.center.y);
      return rect;
    }

    /// <summary>Moves a Rect to the specified center Y position.</summary>
    /// <param name="rect">The desired original Rect.</param>
    /// <param name="y">The desired desired center y position.</param>
    public static Rect SetCenterY(this Rect rect, float y)
    {
      rect.center = new Vector2(rect.center.x, y);
      return rect;
    }

    /// <summary>Moves a Rect to the specified center position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="x">The desired center X position.</param>
    /// <param name="y">The desired center Y position.</param>
    public static Rect SetCenter(this Rect rect, float x, float y)
    {
      rect.center = new Vector2(x, y);
      return rect;
    }

    /// <summary>Moves a Rect to the specified center position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="center">The desired center position.</param>
    public static Rect SetCenter(this Rect rect, Vector2 center)
    {
      rect.center = center;
      return rect;
    }

    /// <summary>Moves a Rect to the specified position.</summary>
    /// <param name="rect">The orignal Rect.</param>
    /// <param name="position">The desired position.</param>
    public static Rect SetPosition(this Rect rect, Vector2 position)
    {
      rect.position = position;
      return rect;
    }

    /// <summary>Resets a Rect's position to zero.</summary>
    /// <param name="rect">The original Rect.</param>
    public static Rect ResetPosition(this Rect rect)
    {
      rect.position = Vector2.zero;
      return rect;
    }

    /// <summary>Moves a Rect's position by the specified amount.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="move">The change in position.</param>
    public static Rect AddPosition(this Rect rect, Vector2 move)
    {
      rect.position += move;
      return rect;
    }

    /// <summary>Moves a Rect's position by the specified amount.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="x">The x.</param>
    /// <param name="y">The y.</param>
    public static Rect AddPosition(this Rect rect, float x, float y)
    {
      rect.x += x;
      rect.y += y;
      return rect;
    }

    /// <summary>Sets a Rect's X position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="x">The desired X position.</param>
    public static Rect SetX(this Rect rect, float x)
    {
      rect.x = x;
      return rect;
    }

    /// <summary>Adds to a Rect's X position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="x">The value to add.</param>
    public static Rect AddX(this Rect rect, float x)
    {
      rect.x += x;
      return rect;
    }

    /// <summary>Subtracts from a Rect's X position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="x">The value to subtract.</param>
    public static Rect SubX(this Rect rect, float x)
    {
      rect.x -= x;
      return rect;
    }

    /// <summary>Sets a Rect's Y position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="y">The desired Y position.</param>
    public static Rect SetY(this Rect rect, float y)
    {
      rect.y = y;
      return rect;
    }

    /// <summary>Adds to a Rect's Y position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="y">The value to add.</param>
    public static Rect AddY(this Rect rect, float y)
    {
      rect.y += y;
      return rect;
    }

    /// <summary>Subtracts a Rect's Y position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="y">The value to subtract.</param>
    public static Rect SubY(this Rect rect, float y)
    {
      rect.y -= y;
      return rect;
    }

    /// <summary>Sets the min position of a Rect.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="min">The desired min position.</param>
    public static Rect SetMin(this Rect rect, Vector2 min)
    {
      rect.min = min;
      return rect;
    }

    /// <summary>Adds to a Rect's min position.</summary>
    /// <param name="rect">The original rect.</param>
    /// <param name="value">The value to add.</param>
    public static Rect AddMin(this Rect rect, Vector2 value)
    {
      rect.min += value;
      return rect;
    }

    /// <summary>Subtracts a Rect's min position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The vlaue to subtract.</param>
    public static Rect SubMin(this Rect rect, Vector2 value)
    {
      rect.min -= value;
      return rect;
    }

    /// <summary>Sets a Rect's max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="max">The desired max position.</param>
    public static Rect SetMax(this Rect rect, Vector2 max)
    {
      rect.max = max;
      return rect;
    }

    /// <summary>Adds to a Rect's max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to add.</param>
    public static Rect AddMax(this Rect rect, Vector2 value)
    {
      rect.max += value;
      return rect;
    }

    /// <summary>Subtracts a Rect's max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to add.</param>
    public static Rect SubMax(this Rect rect, Vector2 value)
    {
      rect.max -= value;
      return rect;
    }

    /// <summary>Sets a Rect's X min position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="xMin">The desired min X position.</param>
    public static Rect SetXMin(this Rect rect, float xMin)
    {
      rect.xMin = xMin;
      return rect;
    }

    /// <summary>Adds to a Rect's X min position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to add.</param>
    public static Rect AddXMin(this Rect rect, float value)
    {
      rect.xMin += value;
      return rect;
    }

    /// <summary>Subtracts from a Rect's X min position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to subtract.</param>
    public static Rect SubXMin(this Rect rect, float value)
    {
      rect.xMin -= value;
      return rect;
    }

    /// <summary>Sets a Rect's X max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="xMax">The desired X max position.</param>
    public static Rect SetXMax(this Rect rect, float xMax)
    {
      rect.xMax = xMax;
      return rect;
    }

    /// <summary>Adds to a Rect's X max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to add.</param>
    public static Rect AddXMax(this Rect rect, float value)
    {
      rect.xMax += value;
      return rect;
    }

    /// <summary>Subtracts a Rect's X max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to subtract.</param>
    public static Rect SubXMax(this Rect rect, float value)
    {
      rect.xMax -= value;
      return rect;
    }

    /// <summary>Sets a Rect's Y min position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="yMin">The desired Y min.</param>
    public static Rect SetYMin(this Rect rect, float yMin)
    {
      rect.yMin = yMin;
      return rect;
    }

    /// <summary>Adds to a Rect's Y min position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to add.</param>
    public static Rect AddYMin(this Rect rect, float value)
    {
      rect.yMin += value;
      return rect;
    }

    /// <summary>Subtracts a Rect's Y min position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to subtract.</param>
    /// <returns></returns>
    public static Rect SubYMin(this Rect rect, float value)
    {
      rect.yMin -= value;
      return rect;
    }

    /// <summary>Sets a Rect's Y max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="yMax">The desired Y max position.</param>
    public static Rect SetYMax(this Rect rect, float yMax)
    {
      rect.yMax = yMax;
      return rect;
    }

    /// <summary>Adds to a Rect's Y max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to add.</param>
    public static Rect AddYMax(this Rect rect, float value)
    {
      rect.yMax += value;
      return rect;
    }

    /// <summary>Subtracts from a Rect's Y max position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="value">The value to subtract.</param>
    public static Rect SubYMax(this Rect rect, float value)
    {
      rect.yMax -= value;
      return rect;
    }

    /// <summary>
    /// Sets a Rect's width, if it is less than the specified value.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="minWidth">The desired min width.</param>
    public static Rect MinWidth(this Rect rect, float minWidth)
    {
      rect.width = Mathf.Max(rect.width, minWidth);
      return rect;
    }

    /// <summary>
    /// Sets a Rect's width, if it is greater than the specified value.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="maxWidth">The desired max width.</param>
    public static Rect MaxWidth(this Rect rect, float maxWidth)
    {
      rect.width = Mathf.Min(rect.width, maxWidth);
      return rect;
    }

    /// <summary>
    /// Sets a Rect's height, if it is less than the specified value.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="minHeight">The desired min height.</param>
    public static Rect MinHeight(this Rect rect, float minHeight)
    {
      rect.height = Mathf.Max(rect.height, minHeight);
      return rect;
    }

    /// <summary>
    /// Sets a Rect's height, if it is greater than the specified value.
    /// </summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="maxHeight">The desired max height.</param>
    public static Rect MaxHeight(this Rect rect, float maxHeight)
    {
      rect.height = Mathf.Min(rect.height, maxHeight);
      return rect;
    }

    /// <summary>Expands a rect to contain a given position.</summary>
    /// <param name="rect">The original Rect.</param>
    /// <param name="pos">The position to expand the rect towards.</param>
    public static Rect ExpandTo(this Rect rect, Vector2 pos)
    {
      if (pos.x < (double)rect.xMin)
        rect.xMin = pos.x;
      else if (pos.x > (double)rect.xMax)
        rect.xMax = pos.x;
      if (pos.y < (double)rect.yMin)
        rect.yMin = pos.y;
      else if (pos.y > (double)rect.yMax)
        rect.yMax = pos.y;
      return rect;
    }
  }
}