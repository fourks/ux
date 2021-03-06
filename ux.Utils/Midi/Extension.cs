﻿/* ux.Utils / Software Synthesizer Library

LICENSE - The MIT License (MIT)

Copyright (c) 2013-2014 Tomona Nanase

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
 */

using System.Xml.Linq;

namespace ux.Utils.Midi
{
    /// <summary>
    /// 拡張メソッドを含んだ静的クラスです。
    /// </summary>
    public static class Extension
    {
        /// <summary>
        /// 要素中の指定された名前を持つ属性値を取得します。
        /// </summary>
        /// <param name="element">属性が属する要素。</param>
        /// <param name="name">属性名。</param>
        /// <returns>属性の値。存在しない場合は null。</returns>
        public static string GetAttribute(this XElement element, XName name)
        {
            var result = element.Attribute(name);
            return result == null ? null : result.Value;
        }
    }
}
