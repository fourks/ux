/* ux - Micro Xylph / Software Synthesizer Core Library
 * Copyright (C) 2013 Tomona Nanase. All rights reserved.
 */

namespace ux.Component
{
    /// <summary>
    /// ���g���ƈʑ�����g�`�𐶐�����E�F�[�u�W�F�l���[�^�̃C���^�t�F�[�X�ł��B
    /// </summary>
	interface IWaveform
	{
		#region Methods
        /// <summary>
        /// �^����ꂽ���g���ƈʑ�����g�`�𐶐����܂��B
        /// </summary>
        /// <param name="data">�������ꂽ�g�`�f�[�^����������z��B</param>
        /// <param name="frequency">�����Ɏg�p�������g���̔z��B</param>
        /// <param name="phase">�����Ɏg�p�����ʑ��̔z��B</param>
        /// <param name="count">�z��ɑ�������f�[�^�̐��B</param>
		void GetWaveforms(float[] data, double[] frequency, double[] phase, int count);

        /// <summary>
        /// �p�����[�^���w�肵�Ĕg�`�̐ݒ�l��ύX���܂��B
        /// </summary>
        /// <param name="parameter">�p�����[�^�I�u�W�F�N�g�ƂȂ� PValue �l�B</param>
		void SetParameter (PValue parameter);
		#endregion
	}
}
