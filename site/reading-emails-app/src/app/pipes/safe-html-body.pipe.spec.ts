import { SafeHtmlBodyPipe } from './safe-html-body.pipe';

describe('SafeHtmlBodyPipe', () => {
  it('create an instance', () => {
    const pipe = new SafeHtmlBodyPipe();
    expect(pipe).toBeTruthy();
  });
});
