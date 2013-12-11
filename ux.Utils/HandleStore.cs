﻿/* uxMidi / Software Synthesizer Library
 * Copyright (C) 2013 Tomona Nanase. All rights reserved.
 */

using System;
using System.Collections.Generic;
using System.IO;
using ux.Component;

namespace ux.Utils
{
    /// <summary>
    /// ストリームからハンドルを読み取り、ディクショナリとしてアクセスできるクラスです。
    /// </summary>
    public class HandleStore : Dictionary<string, IEnumerable<Handle>>
    {
        #region -- Constructors --
        /// <summary>
        /// 新しい HandleStore クラスのインスタンスを初期化します。
        /// </summary>
        public HandleStore()
        {
        }

        /// <summary>
        /// ファイルを読み込み、新しい HandleStore クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="file">読み込まれるファイル名。</param>
        public HandleStore(string file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            if (!this.AddFromFile(file))
                throw new Exception("ハンドルの読み取りに失敗しました。");
        }

        /// <summary>
        /// ストリームを読み込み、新しい HandleStore クラスのインスタンスを初期化します。
        /// </summary>
        /// <param name="stream">読み込まれるストリーム。</param>
        public HandleStore(Stream stream)
        {
            if (stream == null)
                throw new ArgumentNullException("stream");

            if (!stream.CanRead)
                throw new NotSupportedException();

            if (!this.AddFromStream(stream))
                throw new Exception("ハンドルの読み取りに失敗しました。");
        }
        #endregion

        #region -- Public Methods --
        /// <summary>
        /// ファイルからハンドルを読み込み、追加します。
        /// </summary>
        /// <param name="file">読み込まれるファイルの名前。</param>
        /// <returns>正常に追加が完了した場合は true、失敗した場合は false。</returns>
        public bool AddFromFile(string file)
        {
            if (file == null)
                throw new ArgumentNullException("file");

            using (FileStream fs = new FileStream(file, FileMode.Open))
                return this.AddFromStream(fs);
        }

        /// <summary>
        /// ストリームからハンドルを読み込み、追加します。
        /// </summary>
        /// <param name="stream">読み込まれるストリーム。</param>
        /// <returns>正常に追加が完了した場合は true、失敗した場合は false。</returns>
        public bool AddFromStream(Stream stream)
        {
            string[] lines;
            IEnumerable<Handle> handles;

            if (stream == null)
                throw new ArgumentNullException("stream");

            if (!stream.CanRead)
                throw new NotSupportedException();

            using (StreamReader sr = new StreamReader(stream))
                lines = sr.ReadToEnd().Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < lines.Length; i++)
            {
                var key = lines[i].Trim();

                if (!key.StartsWith("[") || !key.EndsWith("]"))
                    return false;

                key = key.Substring(1, key.Length - 2);

                string handle_lines = string.Empty;
                i++;

                for (; i < lines.Length; i++)
                {
                    var handle = lines[i].Trim();

                    if (handle.StartsWith("["))
                    {
                        i--;
                        break;
                    }

                    handle_lines += handle + "\n";
                }

                if (!HandleParser.TryParse(handle_lines, out handles))
                    return false;

                this[key] = handles;
            }

            return true;
        }
        #endregion
    }
}
