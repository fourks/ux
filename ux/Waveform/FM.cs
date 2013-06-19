/* ux - Micro Xylph / Software Synthesizer Core Library
 * Copyright (C) 2013 Tomona Nanase. All rights reserved.
 */
using System;
using ux.Component;

namespace ux.Waveform
{
    /// <summary>
    /// FM (���g���ϒ�) ��p�����g�`�W�F�l���[�^�N���X�ł��B
    /// </summary>
    class FM : IWaveform
    {
        #region Private Field
        private readonly Operator op0, op1, op2, op3;
        #endregion

        #region Constructor
        /// <summary>
        /// �V���� FM �N���X�̃C���X�^���X�����������܂��B
        /// </summary>
        public FM()
        {
            this.op0 = new Operator();
            this.op1 = new Operator();
            this.op2 = new Operator();
            this.op3 = new Operator();

            this.op0.OutAmplifier = 1.0;
            this.op0.Send0 = 0.75;
            this.op1.Send0 = 0.5;
        }
        #endregion

        #region IWaveform implementation
        /// <summary>
        /// �^����ꂽ���g���ƈʑ�����g�`�𐶐����܂��B
        /// </summary>
        /// <param name="data">�������ꂽ�g�`�f�[�^����������z��B</param>
        /// <param name="frequency">�����Ɏg�p�������g���̔z��B</param>
        /// <param name="phase">�����Ɏg�p�����ʑ��̔z��B</param>
        /// <param name="count">�z��ɑ�������f�[�^�̐��B</param>
        public void GetWaveforms(float[] data, double[] frequency, double[] phase, int count)
        {
            double omega, tmp0, tmp1, tmp2, tmp3;

            for (int i = 0; i < count; i++)
            {
                omega = 2.0 * Math.PI * phase[i] * frequency[i];
                tmp0 =
                    Math.Sin(omega * this.op0.FreqFactor +
                    this.op0.Send0 * this.op0.Old +
                    this.op1.Send0 * this.op1.Old +
                    this.op2.Send0 * this.op2.Old +
                    this.op3.Send0 * this.op3.Old) * this.op0.Amplifier;

                tmp1 =
                    Math.Sin(omega * this.op1.FreqFactor +
                    this.op0.Send1 * this.op0.Old +
                    this.op1.Send1 * this.op1.Old +
                    this.op2.Send1 * this.op2.Old +
                    this.op3.Send1 * this.op3.Old) * this.op1.Amplifier;

                tmp2 =
                    Math.Sin(omega * this.op2.FreqFactor +
                    this.op0.Send2 * this.op0.Old +
                    this.op1.Send2 * this.op1.Old +
                    this.op2.Send2 * this.op2.Old +
                    this.op3.Send2 * this.op3.Old) * this.op2.Amplifier;

                tmp3 =
                    Math.Sin(omega * this.op3.FreqFactor +
                    this.op0.Send3 * this.op0.Old +
                    this.op1.Send3 * this.op1.Old +
                    this.op2.Send3 * this.op2.Old +
                    this.op3.Send3 * this.op3.Old) * this.op3.Amplifier;

                this.op0.Old = tmp0;
                this.op1.Old = tmp1;
                this.op2.Old = tmp2;
                this.op3.Old = tmp3;

                data[i] = (float)(this.op0.OutAmplifier * tmp0 +
                    this.op1.OutAmplifier * tmp1 +
                    this.op2.OutAmplifier * tmp2 +
                    this.op3.OutAmplifier * tmp3);
            }
        }

        /// <summary>
        /// �p�����[�^���w�肵�Ĕg�`�̐ݒ�l��ύX���܂��B
        /// </summary>
        /// <param name="parameter">�p�����[�^�I�u�W�F�N�g�ƂȂ� PValue �l�B</param>
        public void SetParameter(PValue parameter)
        {
            if (parameter.Name.Length < 4)
                return;

            Operator op;
            switch (parameter.Name.Substring(0, 3))
            {
                case "op0": op = this.op0; break;
                case "op1": op = this.op1; break;
                case "op2": op = this.op2; break;
                case "op3": op = this.op3; break;
                default:
                    return;
            }

            switch (parameter.Name.Substring(3))
            {
                case "out":
                    op.OutAmplifier = parameter.Value;
                    break;
                case "amp":
                    op.Amplifier = parameter.Value;
                    break;
                case "freq":
                    op.FreqFactor = parameter.Value;
                    break;
                case "send0":
                    op.Send0 = parameter.Value;
                    break;
                case "send1":
                    op.Send1 = parameter.Value;
                    break;
                case "send2":
                    op.Send2 = parameter.Value;
                    break;
                case "send3":
                    op.Send3 = parameter.Value;
                    break;

                default:
                    break;
            }
        }
        #endregion

        /// <summary>
        /// FM ������ 1 ���W���[���ƂȂ�I�y���[�^�N���X�ł��B
        /// </summary>
        class Operator
        {
            /// <summary>
            /// �o�͂ɐڑ�����鑝���x�B
            /// </summary>
            public double OutAmplifier = 0.0f;

            /// <summary>
            /// �e�I�y���[�^�ɑ��M����郌�x���̑����x�B
            /// </summary>
            public double Amplifier = 1.0f;

            /// <summary>
            /// ���̃I�y���[�^�����U������g���̕␳�W���B
            /// </summary>
            public double FreqFactor = 1.0f;

            /// <summary>
            /// �I�y���[�^ 0 �ɑ��M�����g�`�̃��x���B
            /// </summary>
            public double Send0 = 0.0f;

            /// <summary>
            /// �I�y���[�^ 1 �ɑ��M�����g�`�̃��x���B
            /// </summary>
            public double Send1 = 0.0f;

            /// <summary>
            /// �I�y���[�^ 2 �ɑ��M�����g�`�̃��x���B
            /// </summary>
            public double Send2 = 0.0f;

            /// <summary>
            /// �I�y���[�^ 3 �ɑ��M�����g�`�̃��x���B
            /// </summary>
            public double Send3 = 0.0f;

            /// <summary>
            /// �I�y���[�^�����������Â��l�B
            /// </summary>
            public double Old = 0.0f;
        }
    }
}

