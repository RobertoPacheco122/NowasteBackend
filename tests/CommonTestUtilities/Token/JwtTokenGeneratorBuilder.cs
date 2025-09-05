﻿using Moq;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Security.Tokens;

namespace CommonTestUtilities.Token;
public class JwtTokenGeneratorBuilder {
    public static IAccessTokenGenerator Build() {
        var mock = new Mock<IAccessTokenGenerator>();

        mock.Setup(accessTokenGenerator => accessTokenGenerator.Generate(It.IsAny<UserEntity>()))
            .Returns("eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiYWRtaW4iOnRydWUsImlhdCI6MTUxNjIzOTAyMn0.KMUFsIDTnFmyG3nMiGM6H9FNFUROf3wh7SmqJp-QV30");

        return mock.Object;
    }
}
