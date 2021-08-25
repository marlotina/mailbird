import { EmailAddressesPipe } from './email-addresses.pipe';

describe('EmailAddressesPipe', () => {
  it('create an instance', () => {
    const pipe = new EmailAddressesPipe();
    expect(pipe).toBeTruthy();
  });
});
