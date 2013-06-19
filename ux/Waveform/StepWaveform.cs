/* ux - Micro Xylph / Software Synthesizer Core Library
 * Copyright (C) 2013 Tomona Nanase. All rights reserved.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using ux.Component;

namespace ux.Waveform
{
    /// <summary>
    /// �X�e�b�v (�K�i��) �g�`�𐶐��ł���W�F�l���[�^�N���X�ł��B
    /// </summary>
    class StepWaveform : IWaveform
    {
        #region Protected Field
        /// <summary>
        /// �~���� Math.PI �� Single �^�ɃL���X�g�����萔�l�ł��B
        /// </summary>
        protected const float PI = (float)Math.PI;

        /// <summary>
        /// �~���� Math.PI �� 2 �{�� Single �^�ɃL���X�g�����萔�l�ł��B
        /// </summary>
        protected const float PI2 = (float)(Math.PI * 2.0);

        /// <summary>
        /// �~���� Math.PI �� 0.5 �{�� Single �^�ɃL���X�g�����萔�l�ł��B
        /// </summary>
        protected const float PI_2 = (float)(Math.PI * 0.5);

        /// <summary>
        /// �g�`�����ɗp�����鐶�f�[�^�̔z��ł��B
        /// </summary>
        protected float[] value;

        /// <summary>
        /// �g�`�����ɗp������f�[�^���̒����ł��B
        /// </summary>
        protected float length;

        /// <summary>
        /// �g�`�����ɗp��������g���␳�W���ł��B
        /// </summary>
        protected double freqFactor = 1.0;
        #endregion

        #region Private Field
        private Queue<byte> queue = null;
        #endregion

        #region Constructors
        /// <summary>
        /// ��̔g�`�f�[�^���g���ĐV���� StepWaveform �N���X�̃C���X�^���X�����������܂��B
        /// </summary>
        public StepWaveform()
        {
            this.SetStep(new byte[] { 0 });
        }
        #endregion

        #region IWaveform implementation
        /// <summary>
        /// �^����ꂽ���g���ƈʑ�����X�e�b�v�g�`�𐶐����܂��B
        /// </summary>
        /// <param name="data">�������ꂽ�g�`�f�[�^����������z��B</param>
        /// <param name="frequency">�����Ɏg�p�������g���̔z��B</param>
        /// <param name="phase">�����Ɏg�p�����ʑ��̔z��B</param>
        /// <param name="count">�z��ɑ�������f�[�^�̐��B</param>
        public virtual void GetWaveforms(float[] data, double[] frequency, double[] phase, int count)
        {
            float tmp;
            for (int i = 0; i < count; i++)
            {
                tmp = (float)(phase[i] * frequency[i] * freqFactor);
                if (float.IsInfinity(tmp) || float.IsNaN(tmp) || tmp < 0.0f)
                    data[i] = 0.0f;
                else
                    data[i] = this.value[(int)(tmp * this.length) % this.value.Length];
            }
        }

        /// <summary>
        /// �p�����[�^���w�肵�Ă��̔g�`�̐ݒ�l��ύX���܂��B
        /// </summary>
        /// <param name="parameter">�p�����[�^�I�u�W�F�N�g�ƂȂ� PValue �l�B</param>
        public virtual void SetParameter(PValue parameter)
        {
            switch (parameter.Name)
            {
                case "freqfactor":
                    this.freqFactor = parameter.Value * 0.001;
                    break;

                case "begin":
                    this.queue = new Queue<byte>();
                    this.queue.Enqueue((byte)parameter.Value);
                    break;

                case "end":
                    if (this.queue != null)
                    {
                        this.queue.Enqueue((byte)parameter.Value);
                        if (this.queue.Count <= 32767)
                            this.SetStep(this.queue.ToArray());
                    }
                    this.queue = null;
                    break;

                case "queue":
                    if (this.queue != null)
                        this.queue.Enqueue((byte)parameter.Value);
                    break;

                default:
                    break;
            }
        }

        /// <summary>
        /// �w�肳�ꂽ�X�e�b�v�f�[�^����g�`�����p�̃f�[�^���쐬���܂��B
        /// </summary>
        /// <param name="data">�g�`�����̃x�[�X�ƂȂ�X�e�b�v�f�[�^�B</param>
        public void SetStep(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException();
            if (data.Length > 32767)
                throw new ArgumentException("�X�e�b�v�f�[�^�� 32767 �o�C�g�ȉ��łȂ���΂Ȃ�܂���B");

            float max = data.Max(),
            min = data.Min(),
            a = 2.0f / (max - min);
            this.length = data.Length;
            this.value = new float[data.Length];

            if (max == min)
                return;

            for (int i = 0; i < data.Length; i++)
                this.value[i] = (data[i] - min) * a - 1.0f;
        }
        #endregion
    }
}
