﻿using Impulse.Core;
using Impulse.Core.Requests;
using Impulse.Core.Responses;

namespace Impulse.Interfaces
{
    public interface ICvUploadService
    {
        Task<ServiceResult<CvUploadResponse>> CvUpload(CvAddRequest request);
    }
}
